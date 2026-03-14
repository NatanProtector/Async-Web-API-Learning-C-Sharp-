using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.API.Entities
{
    [Table("Books")]
    public class Book
    {
        [Key] // Entity Framework Core will create generate key if not specified
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(2500)]
        public string Description { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        public Author? Author { get; set; }


        public Book(Guid id, string title, string description, Guid authorId)
        {
            Id = id;
            Title = title;
            Description = description;
            AuthorId = authorId;
        }

        public Book(string title, string description, Guid authorId)
        {
            Title = title;
            Description = description;
            AuthorId = authorId;
        }

    }
}