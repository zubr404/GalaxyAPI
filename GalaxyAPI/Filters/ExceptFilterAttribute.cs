using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Filters
{
    public class ExceptFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var innerEx = context.Exception.InnerException?.Message;
            var exStack = context.Exception.StackTrace;
            var exMessage = context.Exception.Message;
            context.Result = new ContentResult
            {
                Content = $"В {actionName} возникло исключение:\n{exMessage}\nInnerExeption message: {innerEx}\n{exStack}",
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }
}
