using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Datatables
{
    public class ResponsiveAltModel : PageModel
    {
        private readonly ILogger<ResponsiveAltModel> _logger;

        public ResponsiveAltModel(ILogger<ResponsiveAltModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
