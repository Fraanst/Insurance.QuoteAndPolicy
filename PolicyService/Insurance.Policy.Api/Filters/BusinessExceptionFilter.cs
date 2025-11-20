using Insurance.Policy.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Insurance.Policy.Api.Filters
{
    public class BusinessExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(QuoteNotApprovedException))
            {
                var exception = context.Exception as QuoteNotApprovedException;

                context.HttpContext.Response.StatusCode = 422;

                context.Result = new JsonResult(new
                {
                    status = 422,
                    title = "Regra de Negócio Violada",
                    detail = exception.Message,
                });

                context.ExceptionHandled = true;
            }

            if (context.Exception.GetType() == typeof(QuoteNotFoundException))
            {
                var notFoundException = context.Exception as QuoteNotFoundException;
                string customMessage = notFoundException.Message;

                context.HttpContext.Response.StatusCode = 404;
                context.Result = new JsonResult(new
                {
                    status = 404,
                    title = "Proposta não encontrada",
                    detail = customMessage,
                });

                context.ExceptionHandled = true;
            }
        }
    }
}
