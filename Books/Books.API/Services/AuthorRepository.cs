using Books.API.Entities;

namespace Books.API.Services
{
    public class AuthorRepository : IAuthorRepository
    {
        public Author GetAuthorById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetAuthorByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Author> GetAuthors()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
