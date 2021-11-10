using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Icons
{
    public class FontawesomeRegularModel : PageModel
    {
        private readonly ILogger<FontawesomeRegularModel> _logger;

        public FontawesomeRegularModel(ILogger<FontawesomeRegularModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
