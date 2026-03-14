using System.ComponentModel.DataAnnotations;

namespace Books.API.Model
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AuthorDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
