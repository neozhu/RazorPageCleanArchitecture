using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class ButtonsModel : PageModel
    {
        private readonly ILogger<ButtonsModel> _logger;

        public ButtonsModel(ILogger<ButtonsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
