using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class TabsPillsModel : PageModel
    {
        private readonly ILogger<TabsPillsModel> _logger;

        public TabsPillsModel(ILogger<TabsPillsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
