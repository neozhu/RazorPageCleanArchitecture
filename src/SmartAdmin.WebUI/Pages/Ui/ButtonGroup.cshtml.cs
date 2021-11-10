using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class ButtonGroupModel : PageModel
    {
        private readonly ILogger<ButtonGroupModel> _logger;

        public ButtonGroupModel(ILogger<ButtonGroupModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
