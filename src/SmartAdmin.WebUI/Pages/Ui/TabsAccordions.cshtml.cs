using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class TabsAccordionsModel : PageModel
    {
        private readonly ILogger<TabsAccordionsModel> _logger;

        public TabsAccordionsModel(ILogger<TabsAccordionsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
