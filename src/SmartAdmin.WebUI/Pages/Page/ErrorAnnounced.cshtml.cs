using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class ErrorAnnouncedModel : PageModel
    {
        private readonly ILogger<ErrorAnnouncedModel> _logger;

        public ErrorAnnouncedModel(ILogger<ErrorAnnouncedModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
