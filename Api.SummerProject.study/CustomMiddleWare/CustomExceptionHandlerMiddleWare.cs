using Domain.Exceptions;
using Shared;
using Shared.ErrorModels;

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
            var Error = new ErrorToReturn()
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = ex.Message
            };


            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnAuthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException => GetBadRequestErrors(badRequestException, Error),
                _ => StatusCodes.Status500InternalServerError,
            };
            await context.Response.WriteAsJsonAsync(Error);
        }
        public static int GetBadRequestErrors(BadRequestException badRequestException, ErrorToReturn errorToReturn)
        {
            errorToReturn.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
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
