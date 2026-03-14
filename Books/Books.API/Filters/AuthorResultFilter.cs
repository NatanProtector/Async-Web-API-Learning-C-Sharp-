using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Books.API.Filters
{
    public class AuthorResultFilter : IAsyncResultFilter
    {
        private readonly IMapper _mapper;
        public AuthorResultFilter(IMapper mapper)
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

            result.Value = _mapper.Map<Model.AuthorDto>(result.Value);

            await next();
        }
    }
}

