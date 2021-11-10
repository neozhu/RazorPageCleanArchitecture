using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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
