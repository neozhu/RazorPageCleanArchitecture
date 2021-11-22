using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Statistics
{
    public class DygraphModel : PageModel
    {
        private readonly ILogger<DygraphModel> _logger;

        public DygraphModel(ILogger<DygraphModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
