using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Statistics
{
    public class C3Model : PageModel
    {
        private readonly ILogger<C3Model> _logger;

        public C3Model(ILogger<C3Model> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
