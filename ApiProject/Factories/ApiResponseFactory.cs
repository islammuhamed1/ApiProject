using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using System.Net;

namespace ApiProject.Factories
{
    public class ApiResponseFactory
    {
        public static ActionResult CustomValidationErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.Where(error => error.Value.Errors.Any())
                                            .Select(error => new ValidationError
                                            {
                                                Key = error.Key,
                                                Errors = error.Value.Errors.Select(x => x.ErrorMessage)
                                            });
         var validationResponse = new ValidationErrorResponse
         {
             StatusCode = (int)HttpStatusCode.BadRequest,
             Errors = errors,
             ErrorMessage= "Validation Failed"
         };
            return new BadRequestObjectResult(validationResponse);
        }
    }
}
