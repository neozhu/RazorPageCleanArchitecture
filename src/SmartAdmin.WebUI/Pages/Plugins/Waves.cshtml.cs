using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Plugins
{
    public class WavesModel : PageModel
    {
        private readonly ILogger<WavesModel> _logger;

        public WavesModel(ILogger<WavesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
