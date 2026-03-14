using AutoMapper;
using Books.API.Model;
using Books.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [ApiController]
    [Route("api/bookcollections")]
    public class BookCollectionsController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IMapper _mapper;

        public BookCollectionsController(IBooksRepository booksRepository, IMapper mapper)
        {
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _booksRepository = booksRepository 
                ??  throw new ArgumentNullException(nameof(booksRepository));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookCollection([FromBody] IEnumerable<BookForCreationDto> books)
        {
            if (books == null)
            {
                return BadRequest();
            }

            var bookEntities = _mapper.Map<IEnumerable<Entities.Book>>(books);

            foreach (var bookEntity in bookEntities)
            {
                _booksRepository.AddBook(bookEntity);
            }

            await _booksRepository.SaveChangesAsync();

            return Ok(); // Return the retrieved book entities for demonstration purposes.
        }
    }
}
