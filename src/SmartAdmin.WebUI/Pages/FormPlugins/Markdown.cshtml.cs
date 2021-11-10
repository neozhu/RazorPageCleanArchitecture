using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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
