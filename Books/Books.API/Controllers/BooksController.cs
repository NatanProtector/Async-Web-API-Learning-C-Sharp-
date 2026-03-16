using AutoMapper;
using Books.API.Entities;
using Books.API.Filters;
using Books.API.Model;
using Books.API.Profiles;
using Books.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IMapper _mapper;
        public BooksController(IBooksRepository booksRepository, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
        }

        [HttpGet("books")]
        [TypeFilter(typeof(BooksResultFilter))]

        public async Task<IActionResult> GetBooks()
        {
            var books = await _booksRepository.GetBooksAsync();
            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
        }

        [HttpGet("booksstream")]
        public async IAsyncEnumerable<BookDto> StreamBooks()
        {
            await foreach (var book in _booksRepository.GetBooksAsyncEnumerable())
            {
                // Delay 1 second to simulate streaming
                await Task.Delay(1000);

                yield return _mapper.Map<BookDto>(book);
            }
        }

        [HttpGet("books/{id}", Name = "GetBook")]
        [TypeFilter(typeof(BookWithCoversResultFilter))]
        public async Task<IActionResult> GetBook(Guid id,
            CancellationToken cancelationToken)
        {
            var book = await _booksRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            //var bookCover = await _booksRepository.GetBookCoverAsync(books.Id);

            // Example usage of processing book covers one by one

            //var bookCovers = await _booksRepository.GetBookCoversProcessAfterWaitForAllAsync(
            var bookCovers = await _booksRepository.GetBookCoversProcessOneByOneAsync(   
                new List<Guid> 
                {
                    Guid.Parse("a290f1ee-6c54-4b01-90e6-d701748f0853"),
                    Guid.Parse("e290f1ee-6c54-4b01-90e6-d701748f0851"),
                    Guid.Parse("f290f1ee-6c54-4b01-90e6-d701748f0852")
                },
                cancelationToken
            );

            return Ok((book, bookCovers));
        }

        [HttpPost("books")]
        [TypeFilter(typeof(BookResultFilter))]

        public async Task<IActionResult> CreateBook([FromBody] BookForCreationDto bookForCreation)
        {
            if (bookForCreation == null)
            {
                return BadRequest();
            }

            var bookEntity = _mapper.Map<Entities.Book>(bookForCreation);
            _booksRepository.AddBook(bookEntity);

            await _booksRepository.SaveChangesAsync();

            var createdBook = await _booksRepository.GetBookByIdAsync(bookEntity.Id);

            return CreatedAtAction("GetBook", new { id = bookEntity.Id }, createdBook);
        }
    }
}
