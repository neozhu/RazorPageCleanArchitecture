using Microsoft.AspNetCore.Mvc.RazorPages;

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
