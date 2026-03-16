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
            IEnumerable<Guid> bookIds, CancellationToken cancelationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            var covers = new List<BookCoverDto>();

            // Adding concelation token to be able to cancel the request if it takes too long
            //using "using" to ensure that the cancelation token source is disposed of properly after use
            using (var cancelationTokenSource =  new CancellationTokenSource())
            {
                using (var linkedCancelationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
                    cancelationTokenSource.Token,
                    cancelationToken))
                {

                    int index_for_testing_cancel = 1;

                    foreach (Guid bookId in bookIds) {
                        string address = $"http://localhost:5054/api/bookcovers/{bookId}";

                        // For testing purposes, we can cancel the request after a certain number of iterations
                        if (index_for_testing_cancel != -1) {
                            if (index_for_testing_cancel == 3) 
                            {
                                Console.WriteLine("Simulating a long request for testing cancellation...");
                                address += "?returnFault=true";
                            }
                            index_for_testing_cancel++;
                        }

                        var response = await httpClient.GetAsync(address,
                            linkedCancelationTokenSource.Token);

                        if (response.IsSuccessStatusCode)
                        {
                            var coverResponse = JsonSerializer.Deserialize<BookCoverDto>(
                            await response.Content.ReadAsStringAsync(linkedCancelationTokenSource.Token),
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            }
                            );
                            if (coverResponse != null)
                                covers.Add(coverResponse);
                        }
                        else
                        {
                            Console.WriteLine($"Failed to retrieve book cover for book with id {bookId}. Status code: {response.StatusCode}");
                            linkedCancelationTokenSource.Cancel();
                        }
                    }
                }
            }


            return covers;
        }

        public async Task<IEnumerable<BookCoverDto>> GetBookCoversProcessAfterWaitForAllAsync(IEnumerable<Guid> bookIds)
        {
            var HttpClient = _httpClientFactory.CreateClient();

            var tasks = new List<Task<HttpResponseMessage>>();

            foreach (Guid bookId in bookIds)
            {
                tasks.Add(HttpClient.GetAsync($"http://localhost:5054/api/bookcovers/{bookId}"));
            }

            var bookTaskResults = await Task.WhenAll(tasks);

            List<BookCoverDto> bookCoverDtos = new List<BookCoverDto>();

            foreach (var bookTaskResult in bookTaskResults) {
                if (bookTaskResult.IsSuccessStatusCode) {
                    // Decirialize the response content to BookCoverDto
                    var bookCoverDto = JsonSerializer.Deserialize<BookCoverDto>(
                        await bookTaskResult.Content.ReadAsStringAsync(),
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }
                    );
                    if (bookCoverDto != null)
                        bookCoverDtos.Add(bookCoverDto);
                }
            }
            return bookCoverDtos;
        }

    }
}