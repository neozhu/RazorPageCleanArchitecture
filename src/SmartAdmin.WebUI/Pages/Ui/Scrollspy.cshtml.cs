using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class ScrollspyModel : PageModel
    {
        private readonly ILogger<ScrollspyModel> _logger;

        public ScrollspyModel(ILogger<ScrollspyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
