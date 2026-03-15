using Books.API.Entities;
using Books.API.Model;

namespace Books.API.Services
{
    public interface IBooksRepository
    {
        Book GetBookById(Guid id);
        IEnumerable<Book> GetBooks();
        Task<Book> GetBookByIdAsync(Guid id);
        Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<Guid> bookIds);
        Task<IEnumerable<Book>> GetBooksAsync();
        void AddBook(Book bookForCreation);
        Task<bool> SaveChangesAsync();
    }
}