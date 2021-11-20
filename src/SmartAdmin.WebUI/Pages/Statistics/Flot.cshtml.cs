using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Statistics
{
    public class FlotModel : PageModel
    {
        private readonly ILogger<FlotModel> _logger;

        public FlotModel(ILogger<FlotModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
