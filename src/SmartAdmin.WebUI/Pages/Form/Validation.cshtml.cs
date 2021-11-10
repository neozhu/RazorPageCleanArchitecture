using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Form
{
    public class ValidationModel : PageModel
    {
        private readonly ILogger<ValidationModel> _logger;

        public ValidationModel(ILogger<ValidationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
