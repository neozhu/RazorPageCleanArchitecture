using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Icons
{
    public class NextgenBaseModel : PageModel
    {
        private readonly ILogger<NextgenBaseModel> _logger;

        public NextgenBaseModel(ILogger<NextgenBaseModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
