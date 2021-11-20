using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.FormPlugins
{
    public class DatepickerModel : PageModel
    {
        private readonly ILogger<DatepickerModel> _logger;

        public DatepickerModel(ILogger<DatepickerModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
