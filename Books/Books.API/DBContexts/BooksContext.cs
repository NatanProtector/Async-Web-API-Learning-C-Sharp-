using Microsoft.EntityFrameworkCore;

namespace Books.API.DBContexts
{
    public class BooksContext: DbContext
    {
        public DbSet<Entities.Book> Books { get; set; }

        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Entities.Author>().HasData(
                new Entities.Author { Id = Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851"), FirstName = "George", LastName = "RR Martin" },
                new Entities.Author { Id = Guid.Parse("c290f1ee-6c54-4b01-90e6-d701748f0852"), FirstName = "Stephan", LastName = "Fry" },
                new Entities.Author { Id = Guid.Parse("b290f1ee-6c54-4b01-90e6-d701748f0853"), FirstName = "JRR", LastName = "Tolkien" }

            );

            modelBuilder.Entity<Entities.Book>().HasData(
                new Entities.Book { Id = Guid.Parse("e290f1ee-6c54-4b01-90e6-d701748f0851"), Title = "A Game of Thrones", Description = "First book in the A Song of Ice and Fire series.", AuthorId = Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851") },
                new Entities.Book { Id = Guid.Parse("f290f1ee-6c54-4b01-90e6-d701748f0852"), Title = "The Name of the Wind", Description = "First book in The Kingkiller Chronicle series.", AuthorId = Guid.Parse("c290f1ee-6c54-4b01-90e6-d701748f0852") },
                new Entities.Book { Id = Guid.Parse("a290f1ee-6c54-4b01-90e6-d701748f0853"), Title = "The Hobbit", Description = "Prequel to The Lord of the Rings.", AuthorId = Guid.Parse("b290f1ee-6c54-4b01-90e6-d701748f0853") }
            );
        }
    }
}
