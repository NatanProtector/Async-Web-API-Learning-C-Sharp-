using Books.API.Entities;

namespace Books.API.Services
{
    public interface IAuthorRepository

    {
        Author GetAuthorById(Guid id);
        IEnumerable<Author> GetAuthors();
        Task<Author?> GetAuthorByIdAsync(Guid id);
        Task<IEnumerable<Author>> GetAuthorsAsync();
    }
}
