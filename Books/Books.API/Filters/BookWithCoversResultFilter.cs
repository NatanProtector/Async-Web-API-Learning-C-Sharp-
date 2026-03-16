using AutoMapper;
using Books.API.Entities;
using Books.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Books.API.Filters
{
    public class BookWithCoversResultFilter : IAsyncResultFilter
    {
        private readonly IMapper _mapper;
        public BookWithCoversResultFilter(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;
            if (result.Value == null ||
                result.StatusCode < 200 || result.StatusCode >= 300)
            {
                await next();
                return;
            }

            var (book, bookCovers) = ((
                Book book,
                IEnumerable<BookCoverDto> bookCovers))result.Value;

            result.Value = _mapper.Map<BookWithCoversDto>((book, bookCovers));

            await next();
        }
    }
}

