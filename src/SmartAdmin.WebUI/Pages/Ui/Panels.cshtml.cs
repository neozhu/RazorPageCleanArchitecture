using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class PanelsModel : PageModel
    {
        private readonly ILogger<PanelsModel> _logger;

        public PanelsModel(ILogger<PanelsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
