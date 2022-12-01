using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Documentation
{
    public class GettingStartedModel : PageModel
    {
        private readonly ILogger<GettingStartedModel> _logger;

        public GettingStartedModel(ILogger<GettingStartedModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
