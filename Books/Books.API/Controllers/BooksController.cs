using Books.API.Filters;
using Books.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;
        public BooksController(IBooksRepository booksRepository)
        {
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

        [HttpGet("books/{id}")]
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
    }
}
