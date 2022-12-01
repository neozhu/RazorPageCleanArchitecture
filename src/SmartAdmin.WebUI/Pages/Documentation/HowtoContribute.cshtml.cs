using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Documentation
{
    public class HowtoContributeModel : PageModel
    {
        private readonly ILogger<HowtoContributeModel> _logger;

        public HowtoContributeModel(ILogger<HowtoContributeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
