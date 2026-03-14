using AutoMapper;
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

        [HttpGet("books/{id}", Name = "GetBook")]
        [TypeFilter(typeof(BookResultFilter))]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var books = await _booksRepository.GetBookByIdAsync(id);
            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
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
