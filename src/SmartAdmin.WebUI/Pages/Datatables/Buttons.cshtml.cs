using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Datatables
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
