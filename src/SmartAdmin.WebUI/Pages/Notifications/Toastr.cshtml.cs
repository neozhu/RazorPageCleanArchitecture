using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Notifications
{
    public class ToastrModel : PageModel
    {
        private readonly ILogger<ToastrModel> _logger;

        public ToastrModel(ILogger<ToastrModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
