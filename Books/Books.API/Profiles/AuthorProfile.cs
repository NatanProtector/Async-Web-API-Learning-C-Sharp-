using AutoMapper;

namespace Books.API.Profiles
{
    public class AuthorProfile: Profile
    {
        public AuthorProfile()
        {
            CreateMap<Entities.Author, Model.AuthorDto>()
                .ConstructUsing(src => new Model.AuthorDto(src.Id, $"{src.FirstName} {src.LastName}"));
        }
    }
}
