namespace Books.API.Model.External
{
    public class BookCoverDto
    {
        public string Id { get; set; }
        public byte[] Cover { get; set; }

        public BookCoverDto(string id, byte[] cover)
        {
            Id = id;
            Cover = cover;
        }
    }
}
