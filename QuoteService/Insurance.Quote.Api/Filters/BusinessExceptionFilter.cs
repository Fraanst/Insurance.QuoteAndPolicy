using Insurance.Quote.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Insurance.Quote.Api.Filters
{
    public class BusinessExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var detail = context.Exception.Message;
            var status = 500;
            var title = "Erro no Servidor";

            if (context.Exception.GetType() == typeof(QuoteException))
                context.HttpContext.Response.StatusCode = 500;

            if (context.Exception.GetType() == typeof(QuoteStatusChangeFailedException))
            {
                context.HttpContext.Response.StatusCode = 422;
                status = 422;
                title = "Não é possível alterar o status da cotação";
            }
            
            if (context.Exception.GetType() == typeof(QuoteNotFoundException))
            {
                context.HttpContext.Response.StatusCode = 404;
                status = 404;
                title = "Cotação não encontrada";
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
