using Microsoft.AspNetCore.Mvc.RazorPages;

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
