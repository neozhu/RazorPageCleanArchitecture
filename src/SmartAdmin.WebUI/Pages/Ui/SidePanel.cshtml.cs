using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class SidePanelModel : PageModel
    {
        private readonly ILogger<SidePanelModel> _logger;

        public SidePanelModel(ILogger<SidePanelModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
