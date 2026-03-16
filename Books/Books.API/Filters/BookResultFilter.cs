using AutoMapper;
using Books.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Books.API.Filters
{
    public class BookResultFilter : IAsyncResultFilter
    {
        private readonly IMapper _mapper;
        public BookResultFilter(IMapper mapper)
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

            if (result.Value is IEnumerable<Book>)
            {
                result.Value = _mapper.Map<IEnumerable<Model.BookDto>>(result.Value);
            }
            else if (result.Value is Book)
            {
                result.Value = _mapper.Map<Model.BookDto>(result.Value);
            }

            await next(); 
        }
    }
}
