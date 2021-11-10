using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Docs
{
    public class BuildnotesModel : PageModel
    {
        private readonly ILogger<BuildnotesModel> _logger;

        public BuildnotesModel(ILogger<BuildnotesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
