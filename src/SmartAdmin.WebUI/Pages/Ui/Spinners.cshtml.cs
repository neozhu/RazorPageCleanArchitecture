using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class SpinnersModel : PageModel
    {
        private readonly ILogger<SpinnersModel> _logger;

        public SpinnersModel(ILogger<SpinnersModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
