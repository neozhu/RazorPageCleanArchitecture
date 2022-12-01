using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Plugins
{
    public class AppcoreModel : PageModel
    {
        private readonly ILogger<AppcoreModel> _logger;

        public AppcoreModel(ILogger<AppcoreModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
