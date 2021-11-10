using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Docs
{
    public class FlavorsEditionsModel : PageModel
    {
        private readonly ILogger<FlavorsEditionsModel> _logger;

        public FlavorsEditionsModel(ILogger<FlavorsEditionsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
