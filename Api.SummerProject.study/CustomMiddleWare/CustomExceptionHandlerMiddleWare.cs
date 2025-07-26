using Shared;

namespace Api.SummerProject.study.CustomMiddleWare
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next,ILogger<CustomExceptionHandlerMiddleWare> Logger)
        {
            _next = Next;
            _logger = Logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                context.Response.ContentType = "application/json";

                var Error = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = ex.Message
                };

                await context.Response.WriteAsJsonAsync(Error);   //Convert Any Type To Json automaticly
            }
        }

    }
}
