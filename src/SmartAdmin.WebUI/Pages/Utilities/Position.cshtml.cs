using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class PositionModel : PageModel
    {
        private readonly ILogger<PositionModel> _logger;

        public PositionModel(ILogger<PositionModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
