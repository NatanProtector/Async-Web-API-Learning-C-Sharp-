using Books.API.DBContexts;
using Books.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Services
{
    public class BooksRepository : IBooksRepository
    {

        private readonly BooksContext _context;

        public BooksRepository(BooksContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddBook(Book bookForCreation)
        {
            _context.Books.Add(bookForCreation);
        }

        public Book GetBookById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Book> GetBookByIdAsync(Guid id)
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
    }
}