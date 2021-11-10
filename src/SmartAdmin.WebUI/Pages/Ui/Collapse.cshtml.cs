using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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
