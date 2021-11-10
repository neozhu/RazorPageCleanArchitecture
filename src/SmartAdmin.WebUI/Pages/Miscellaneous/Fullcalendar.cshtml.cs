using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Miscellaneous
{
    public class FullcalendarModel : PageModel
    {
        private readonly ILogger<FullcalendarModel> _logger;

        public FullcalendarModel(ILogger<FullcalendarModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
