 using FoodDelivery.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodDelivery.Filters
{
  public class ApiExceptionFilter : ExceptionFilterAttribute
  {
    public override void OnException(ExceptionContext context)
    {
      ApiError apiError = new ApiError();

      if (context.Exception is ApiException ex) {
        context.HttpContext.Response.StatusCode = ex.StatusCode;
      }
      else {
        context.HttpContext.Response.StatusCode = 500;
      }

      apiError.Message = context.Exception.Message;
      apiError.Detail = context.Exception.StackTrace;
      context.Result = new JsonResult(apiError);

      base.OnException(context);
    }
  }
}