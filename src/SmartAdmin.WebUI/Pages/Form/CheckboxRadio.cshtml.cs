using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Form
{
    public class CheckboxRadioModel : PageModel
    {
        private readonly ILogger<CheckboxRadioModel> _logger;

        public CheckboxRadioModel(ILogger<CheckboxRadioModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
