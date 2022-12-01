using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Plugins
{
    public class SlimscrollModel : PageModel
    {
        private readonly ILogger<SlimscrollModel> _logger;

        public SlimscrollModel(ILogger<SlimscrollModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
