using Books.API.DBContexts;
using Books.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Services
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BooksContext _context;
        public AuthorRepository(BooksContext context)
        {
            _context = context 
                ?? throw new ArgumentNullException(nameof(context));
        }
        public Author GetAuthorById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Author?> GetAuthorByIdAsync(Guid id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public IEnumerable<Author> GetAuthors()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }
    }
}
