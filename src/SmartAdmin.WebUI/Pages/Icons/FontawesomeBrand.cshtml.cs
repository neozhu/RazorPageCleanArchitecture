using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Icons
{
    public class FontawesomeBrandModel : PageModel
    {
        private readonly ILogger<FontawesomeBrandModel> _logger;

        public FontawesomeBrandModel(ILogger<FontawesomeBrandModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
