using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class InboxGeneralModel : PageModel
    {
        private readonly ILogger<InboxGeneralModel> _logger;

        public InboxGeneralModel(ILogger<InboxGeneralModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
