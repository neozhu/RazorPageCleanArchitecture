using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class LockedModel : PageModel
    {
        private readonly ILogger<LockedModel> _logger;

        public LockedModel(ILogger<LockedModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
