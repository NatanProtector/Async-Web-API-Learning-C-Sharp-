using Books.API.DBContexts;
using Books.API.Entities;
using Books.API.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace Books.API.Services
{
    public class BooksRepository : IBooksRepository
    {

        private readonly BooksContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public BooksRepository(BooksContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public void AddBook(Book bookForCreation)
        {
            _context.Books.Add(bookForCreation);
        }

        public Book GetBookById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Book?> GetBookByIdAsync(Guid id)
        {
            var book = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
            return book;
        }

        public IEnumerable<Book> GetBooks()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Author) // Include the related Author entity
                .ToListAsync();
        }

        public IAsyncEnumerable<Book> GetBooksAsyncEnumerable()
        {
            return _context.Books
                .Include(b => b.Author) // Include the related Author entity
                .AsAsyncEnumerable();
        }
        public async Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<Guid> bookIds)
        {
            return await _context.Books
                .Include(b => b.Author) // Include the related Author entity
                .Where(b => bookIds.Contains(b.Id))
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<BookCoverDto?> GetBookCoverAsync(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync($"http://localhost:5054/api/bookcovers/{id}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to retrieve book cover for book with id {id}. Status code: {response.StatusCode}");
                return null;
            }

            return JsonSerializer.Deserialize<BookCoverDto>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
        public async Task<IEnumerable<BookCoverDto>> GetBookCoversProcessOneByOneAsync(
            IEnumerable<Guid> bookIds)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            var covers = new List<BookCoverDto>();

            foreach (Guid bookId in bookIds) {
                var response = await httpClient.GetAsync($"http://localhost:5054/api/bookcovers/{bookId}");

                if (response.IsSuccessStatusCode)
                {
                    var coverResponse = JsonSerializer.Deserialize<BookCoverDto>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }
                    );
                    if (coverResponse != null)
                        covers.Add(coverResponse);
                }
            }

            return covers;
        }

    }
}