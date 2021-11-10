using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.FormPlugins
{
    public class InputmaskModel : PageModel
    {
        private readonly ILogger<InputmaskModel> _logger;

        public InputmaskModel(ILogger<InputmaskModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
