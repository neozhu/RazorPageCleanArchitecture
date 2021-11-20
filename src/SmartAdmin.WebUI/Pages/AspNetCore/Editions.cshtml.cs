using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.AspNetCore
{
    public class EditionsModel : PageModel
    {
        private readonly ILogger<EditionsModel> _logger;

        public EditionsModel(ILogger<EditionsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
