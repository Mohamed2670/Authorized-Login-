using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MwTesting.Filters
{
    public class LogActivityFilter : IActionFilter
    {
        private readonly ILogger<LogActivityFilter> _logger;
        public LogActivityFilter(ILogger<LogActivityFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            
            _logger.LogInformation($"Information {context.ActionDescriptor.DisplayName} proccesing on controller {context.Controller} with arg { JsonSerializer.Serialize(context.ActionArguments)} :)");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"Information {context.ActionDescriptor.DisplayName} ended on controller {context.Controller} :)");
        }


    }
}