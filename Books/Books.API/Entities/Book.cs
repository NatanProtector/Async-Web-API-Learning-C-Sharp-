using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.API.Entities
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(2500)]
        public string Description { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        public Author Author { get; set; } = null;

        public Book()
        {
        }

        public Book(Guid id, string title, string description, Guid authorId, Author author)
        {
            Id = id;
            Title = title;
            Description = description;
            AuthorId = authorId;
            Author = author;
        }
    }
}