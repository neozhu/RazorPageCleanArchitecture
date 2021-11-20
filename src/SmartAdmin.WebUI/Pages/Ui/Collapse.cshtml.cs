using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class CollapseModel : PageModel
    {
        private readonly ILogger<CollapseModel> _logger;

        public CollapseModel(ILogger<CollapseModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
