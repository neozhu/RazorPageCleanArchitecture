using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class AlertsModel : PageModel
    {
        private readonly ILogger<AlertsModel> _logger;

        public AlertsModel(ILogger<AlertsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
