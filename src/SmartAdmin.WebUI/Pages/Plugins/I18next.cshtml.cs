using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Plugins
{
    public class I18nextModel : PageModel
    {
        private readonly ILogger<I18nextModel> _logger;

        public I18nextModel(ILogger<I18nextModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
