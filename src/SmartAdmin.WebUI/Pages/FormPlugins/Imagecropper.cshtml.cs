using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.FormPlugins
{
    public class ImagecropperModel : PageModel
    {
        private readonly ILogger<ImagecropperModel> _logger;

        public ImagecropperModel(ILogger<ImagecropperModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
