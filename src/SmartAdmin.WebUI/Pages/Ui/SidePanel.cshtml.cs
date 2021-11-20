using Microsoft.AspNetCore.Mvc.RazorPages;

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
