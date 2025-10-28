using Microsoft.AspNetCore.Mvc.Filters;
using StockApp.Controllers;

namespace StockApp.Filters.ActionFilters
{
    public class CreateOrderFilterFactory : Attribute, IFilterFactory
    {
        public bool IsReusable => true;
        private int Order { get; set; }

        public CreateOrderFilterFactory(int order = 0)
        {
            Order = order;
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            CreateOrderActionFilter? filter = serviceProvider.GetRequiredService<CreateOrderActionFilter>();

            filter.Order = Order;

            return filter;
        }
    }

    public class CreateOrderActionFilter : IAsyncActionFilter, IOrderedFilter
    {
        public int Order { get; set; }
        private readonly ILogger<CreateOrderActionFilter> _logger;

        public CreateOrderActionFilter(ILogger<CreateOrderActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(context.Controller is TradeController tradeController)
            {
                if(context.ActionArguments.TryGetValue("orderRequest", out var orderRequest))
                {
                    if(!tradeController.ModelState.IsValid)
                    {
                        _logger.LogError("{ControllerName} - Model State invalid", nameof(TradeController));
                        tradeController.ViewBag.Errors = tradeController.ModelState.Values.SelectMany(p => p.Errors).Select(e => e.ErrorMessage).ToList();

                        context.Result = tradeController.RedirectToAction("Index");
                        return;
                    }
                }
            }

            await next();
        }
    }
}
