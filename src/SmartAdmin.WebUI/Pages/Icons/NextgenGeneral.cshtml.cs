using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Icons
{
    public class NextgenGeneralModel : PageModel
    {
        private readonly ILogger<NextgenGeneralModel> _logger;

        public NextgenGeneralModel(ILogger<NextgenGeneralModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
