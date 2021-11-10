using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class ContactsModel : PageModel
    {
        private readonly ILogger<ContactsModel> _logger;

        public ContactsModel(ILogger<ContactsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
