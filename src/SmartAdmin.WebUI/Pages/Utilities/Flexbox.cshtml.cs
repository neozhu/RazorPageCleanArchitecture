using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class FlexboxModel : PageModel
    {
        private readonly ILogger<FlexboxModel> _logger;

        public FlexboxModel(ILogger<FlexboxModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
