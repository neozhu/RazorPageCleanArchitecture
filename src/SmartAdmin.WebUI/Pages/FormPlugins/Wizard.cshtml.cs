using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.FormPlugins
{
    public class WizardModel : PageModel
    {
        private readonly ILogger<WizardModel> _logger;

        public WizardModel(ILogger<WizardModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
