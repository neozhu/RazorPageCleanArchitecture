using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Plugins
{
    public class PacejsModel : PageModel
    {
        private readonly ILogger<PacejsModel> _logger;

        public PacejsModel(ILogger<PacejsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
