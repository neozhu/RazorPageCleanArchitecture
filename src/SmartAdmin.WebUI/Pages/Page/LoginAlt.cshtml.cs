using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class LoginAltModel : PageModel
    {
        private readonly ILogger<LoginAltModel> _logger;

        public LoginAltModel(ILogger<LoginAltModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
