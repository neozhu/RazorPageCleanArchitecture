using Microsoft.AspNetCore.Mvc.RazorPages;

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
