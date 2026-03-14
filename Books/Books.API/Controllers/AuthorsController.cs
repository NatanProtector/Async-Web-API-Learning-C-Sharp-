using Books.API.Filters;
using Books.API.Model;
using Books.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        }

        [HttpGet]
        [TypeFilter(typeof(AuthorsResultFilter))]
        public async Task<IActionResult> GetAuthors()
        {

            var authors = await _authorRepository.GetAuthorsAsync();
            if (authors == null)
            {
                return NotFound();
            }
            return Ok(authors);
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(AuthorResultFilter))]
        public async Task<IActionResult> GetAuthor(Guid id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);

        }

    }
}
