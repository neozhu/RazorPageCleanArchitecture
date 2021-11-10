using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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
