using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.FormPlugins
{
    public class MarkdownModel : PageModel
    {
        private readonly ILogger<MarkdownModel> _logger;

        public MarkdownModel(ILogger<MarkdownModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
