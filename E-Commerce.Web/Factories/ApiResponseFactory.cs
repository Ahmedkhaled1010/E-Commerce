using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorsResponse(ActionContext context)
        {
            var Errors = context.ModelState.Where(m => m.Value.Errors.Any()).
                   Select(m => new ValidationError()
                   {
                       Field = m.Key,
                       Errors = m.Value.Errors.Select(e => e.ErrorMessage)
                   });
            var Response = new ValidationErrorToReturn()
            {
                ValidationErrors = Errors
            };
            return new BadRequestObjectResult(Response);
        }
    }
}
