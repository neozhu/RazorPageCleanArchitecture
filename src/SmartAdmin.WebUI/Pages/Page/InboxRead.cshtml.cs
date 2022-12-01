using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class InboxReadModel : PageModel
    {
        private readonly ILogger<InboxReadModel> _logger;

        public InboxReadModel(ILogger<InboxReadModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
