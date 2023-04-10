using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace OnlineShop.Filters
{
    public class ParseBadRequest : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Result is not IStatusCodeActionResult result)
            {
                return;
            }
            var statusCode = result.StatusCode;
            if (statusCode == 400)
            {
                var response = new List<string?>();
                var badRequestObjectResult = context.Result as BadRequestObjectResult;
                if (badRequestObjectResult is string)
                {
                    if (badRequestObjectResult.Value != null) response.Add(badRequestObjectResult.Value.ToString());
                }
                else if (badRequestObjectResult?.Value is IEnumerable<IdentityError> errors)
                {
                    foreach (var error in errors)
                        response.Add(error.Description);
                }
                else
                {
                    foreach (var key in context.ModelState.Keys)
                    {
                        foreach (var error in context.ModelState[key].Errors)
                        {
                            response.Add($"{key}: {error.ErrorMessage}");
                        }
                    }

                }


                context.Result = new BadRequestObjectResult(response);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
