using AutoMapper;

namespace Books.API.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<Entities.Book, Model.BookDto>()
               .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => 
                     $"{src.Author.FirstName} {src.Author.LastName}"
               ))
               .ConstructUsing(src => new Model.BookDto(src.Id, string.Empty, src.Title, src.Description));
        }
    }
}
