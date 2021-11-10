using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.AspNetCore
{
    public class FaqModel : PageModel
    {
        private readonly ILogger<FaqModel> _logger;

        public FaqModel(ILogger<FaqModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
