using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class BadgesModel : PageModel
    {
        private readonly ILogger<BadgesModel> _logger;

        public BadgesModel(ILogger<BadgesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
