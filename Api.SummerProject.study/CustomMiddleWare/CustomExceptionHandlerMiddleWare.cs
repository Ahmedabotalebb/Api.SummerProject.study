using Domain.Exceptions;
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
                await NotFountEndPointAsync(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);


                //context.Response.ContentType = "application/json";

                await HandlingExceptionAsync(context, ex);   //Convert Any Type To Json automaticly
            }
        }

        private static async Task HandlingExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError,
            };

            var Error = new ErrorToReturn()
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = ex.Message
            };

            await context.Response.WriteAsJsonAsync(Error);
        }

        private static async Task NotFountEndPointAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"The End Point {context.Request.Path} is not found"
                };
                await context.Response.WriteAsJsonAsync(Response);
            }
        } 
    }
}
