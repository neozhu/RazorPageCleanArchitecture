using Microsoft.AspNetCore.Mvc.RazorPages;

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
