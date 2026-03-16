using AutoMapper;
using Books.API.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;

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

            CreateMap<Model.BookForCreationDto, Entities.Book>()
                .ConstructUsing(src => new Entities.Book(Guid.Empty, src.Title, src.Description, src.AuthorId));

            CreateMap<(Entities.Book book, IEnumerable<Model.BookCoverDto> bookCovers), Model.BookWithCoversDto>()
                .ConstructUsing(src => new Model.BookWithCoversDto(
                    src.book.Id,
                    $"{src.book.Author.FirstName} {src.book.Author.LastName}",
                    src.book.Title,
                    src.book.Description))
                .ForMember(dest => dest.BookCovers, opt => opt.MapFrom(src => src.bookCovers));

            //CreateMap<IEnumerable<Model.BookForCreationDto>, IEnumerable<Entities.Book>>();

            CreateMap<Model.External.BookCoverDto, Model.BookCoverDto>();

            CreateMap<IEnumerable<Model.External.BookCoverDto>, Model.BookWithCoversDto>()
                .ForMember(dest => dest.BookCovers,
                opt => opt.MapFrom(src => src));
        }
    }
}
