using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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
