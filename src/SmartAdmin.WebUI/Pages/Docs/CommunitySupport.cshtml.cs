using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Docs
{
    public class CommunitySupportModel : PageModel
    {
        private readonly ILogger<CommunitySupportModel> _logger;

        public CommunitySupportModel(ILogger<CommunitySupportModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
