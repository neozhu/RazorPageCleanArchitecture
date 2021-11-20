using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class TooltipsPopoversModel : PageModel
    {
        private readonly ILogger<TooltipsPopoversModel> _logger;

        public TooltipsPopoversModel(ILogger<TooltipsPopoversModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
