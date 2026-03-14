using Books.API.Entities;

namespace Books.API.Services
{
    public interface IBooksRepository
    {
        Book GetBookById(Guid id);
        IEnumerable<Book> GetBooks();
        Task<Book> GetBookByIdAsync(Guid id);
        Task<IEnumerable<Book>> GetBooksAsync();
    }
}