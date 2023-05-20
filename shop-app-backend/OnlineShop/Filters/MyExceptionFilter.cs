using Microsoft.AspNetCore.Mvc.Filters;

namespace ShopPortal.Filters
{
    public class MyExceptionFilter :ExceptionFilterAttribute
    {
        private readonly ILogger<MyExceptionFilter> _logger;

        public MyExceptionFilter(ILogger<MyExceptionFilter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception,context.Exception.Message);
            base.OnException(context);
        }
    }
}
