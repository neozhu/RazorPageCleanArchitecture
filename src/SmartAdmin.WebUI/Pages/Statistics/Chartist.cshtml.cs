using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Statistics
{
    public class ChartistModel : PageModel
    {
        private readonly ILogger<ChartistModel> _logger;

        public ChartistModel(ILogger<ChartistModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
