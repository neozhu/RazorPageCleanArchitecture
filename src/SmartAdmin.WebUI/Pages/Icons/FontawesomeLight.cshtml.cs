using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Icons
{
    public class FontawesomeLightModel : PageModel
    {
        private readonly ILogger<FontawesomeLightModel> _logger;

        public FontawesomeLightModel(ILogger<FontawesomeLightModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
