using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Statistics
{
    public class PeityModel : PageModel
    {
        private readonly ILogger<PeityModel> _logger;

        public PeityModel(ILogger<PeityModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
