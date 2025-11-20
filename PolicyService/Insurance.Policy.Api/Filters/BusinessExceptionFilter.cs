using Insurance.Policy.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Insurance.Policy.Api.Filters
{
    public class BusinessExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var detail = context.Exception.Message;
            var status = 500;
            var title = "Erro no Servidor";

            if (context.Exception.GetType() == typeof(PolicyException))
                context.HttpContext.Response.StatusCode = 500;

            if (context.Exception.GetType() == typeof(QuoteNotApprovedException))
            {
                context.HttpContext.Response.StatusCode = 422;
                status = 422;
                title = "Regra de Negócio Violada";
            }

            if (context.Exception.GetType() == typeof(QuoteNotFoundException))
            {
                context.HttpContext.Response.StatusCode = 404;
                status = 404;
                title = "Proposta não encontrada";
            }

            context.Result = new JsonResult(new
            {
                status,
                title,
                detail,
            });

            context.ExceptionHandled = true;
        }
    }
}
