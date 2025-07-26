using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace Api.SummerProject.study.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorRespronse(ActionContext context)
        {
            var Errors = context.ModelState.Where(E => E.Value.Errors.Any())
            .Select(M => new ValidationError()
        {
             Field = M.Key,
             Errors = M.Value.Errors.Select(E => E.ErrorMessage)
        });
            var response = new ValidationToReturn()
            {
                ValidationErrors = Errors
            };
            return new BadRequestObjectResult(response);

        }
    }
}
