using Microsoft.EntityFrameworkCore;

namespace Books.API.DBContexts
{
    public class BooksContext: DbContext
    {
        public DbSet<Entities.Book> Books { get; set; }
        public DbSet<Entities.Author> Authors { get; set; }
        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Entities.Author>().HasData(
                new Entities.Author(Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851"), "George", "RR Martin"),
                new Entities.Author(Guid.Parse("c290f1ee-6c54-4b01-90e6-d701748f0852"), "Stephan", "Fry"),
                new Entities.Author(Guid.Parse("b290f1ee-6c54-4b01-90e6-d701748f0853"), "JRR", "Tolkien")
            );

            modelBuilder.Entity<Entities.Book>().HasData(
                new Entities.Book(Guid.Parse("e290f1ee-6c54-4b01-90e6-d701748f0851"), "A Game of Thrones", "First book in the A Song of Ice and Fire series.", Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851")),
                new Entities.Book(Guid.Parse("f290f1ee-6c54-4b01-90e6-d701748f0852"), "The Name of the Wind", "First book in The Kingkiller Chronicle series.", Guid.Parse("c290f1ee-6c54-4b01-90e6-d701748f0852")),
                new Entities.Book(Guid.Parse("a290f1ee-6c54-4b01-90e6-d701748f0853"), "The Hobbit", "Prequel to The Lord of the Rings.", Guid.Parse("b290f1ee-6c54-4b01-90e6-d701748f0853"))
            );
        }
    }
}
