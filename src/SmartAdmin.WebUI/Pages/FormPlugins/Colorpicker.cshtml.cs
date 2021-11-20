using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.FormPlugins
{
    public class ColorpickerModel : PageModel
    {
        private readonly ILogger<ColorpickerModel> _logger;

        public ColorpickerModel(ILogger<ColorpickerModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
