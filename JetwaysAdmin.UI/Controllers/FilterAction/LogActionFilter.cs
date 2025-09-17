using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace JetwaysAdmin.UI.Controllers.CustomeFilter
{
    public class LogActionFilter 
    {
        //private readonly ILogger<LogActionFilter> _logger;

        //public LogActionFilter(ILogger<LogActionFilter> logger)
        //{
        //    _logger = logger;
        //}

        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    var actionName = context.ActionDescriptor.DisplayName;
        //    _logger.LogInformation($"[START] Executing action: {actionName} at {DateTime.UtcNow}");
        //}

        //public void OnActionExecuted(ActionExecutedContext context)
        //{
        //    var actionName = context.ActionDescriptor.DisplayName;
        //    _logger.LogInformation($"[END] Finished executing action: {actionName} at {DateTime.UtcNow}");
        //}
    }
}
