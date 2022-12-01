using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Documentation
{
    public class ChangelogModel : PageModel
    {
        private readonly ILogger<ChangelogModel> _logger;

        public ChangelogModel(ILogger<ChangelogModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
