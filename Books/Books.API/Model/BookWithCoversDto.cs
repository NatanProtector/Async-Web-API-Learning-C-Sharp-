namespace Books.API.Model
{
    public class BookWithCoversDto : BookDto
    {
        public IEnumerable<BookCoverDto> BookCovers { get; set; }    
        public BookWithCoversDto(Guid id, string authorName, string title, string? description = null)
            : base(id, authorName, title, description)
        {

        }
    }
}
