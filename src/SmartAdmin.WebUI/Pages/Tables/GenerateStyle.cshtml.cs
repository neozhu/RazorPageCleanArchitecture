using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Tables
{
    public class GenerateStyleModel : PageModel
    {
        private readonly ILogger<GenerateStyleModel> _logger;

        public GenerateStyleModel(ILogger<GenerateStyleModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
