using Microsoft.AspNetCore.Mvc.RazorPages;

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
