using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // Log the exception
            _logger.LogError(context.Exception, context.Exception.Message);

            if (context.Exception is UnauthorizedAccessException)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                context.Result = new ObjectResult("An error occurred")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            context.ExceptionHandled = true; // Mark the exception as handled
        }
    }
}
