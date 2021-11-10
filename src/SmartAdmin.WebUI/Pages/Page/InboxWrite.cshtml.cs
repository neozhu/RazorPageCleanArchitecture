using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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
