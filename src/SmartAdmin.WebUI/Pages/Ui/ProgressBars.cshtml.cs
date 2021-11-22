using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class ProgressBarsModel : PageModel
    {
        private readonly ILogger<ProgressBarsModel> _logger;

        public ProgressBarsModel(ILogger<ProgressBarsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
