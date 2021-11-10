using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Icons
{
    public class FontawesomeSolidModel : PageModel
    {
        private readonly ILogger<FontawesomeSolidModel> _logger;

        public FontawesomeSolidModel(ILogger<FontawesomeSolidModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
