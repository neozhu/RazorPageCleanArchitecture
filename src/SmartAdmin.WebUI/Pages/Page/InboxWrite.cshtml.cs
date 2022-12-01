using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class InboxWriteModel : PageModel
    {
        private readonly ILogger<InboxWriteModel> _logger;

        public InboxWriteModel(ILogger<InboxWriteModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
