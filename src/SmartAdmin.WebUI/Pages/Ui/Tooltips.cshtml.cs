using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class TooltipsModel : PageModel
    {
        private readonly ILogger<TooltipsModel> _logger;

        public TooltipsModel(ILogger<TooltipsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
