using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class ConfirmationModel : PageModel
    {
        private readonly ILogger<ConfirmationModel> _logger;

        public ConfirmationModel(ILogger<ConfirmationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
