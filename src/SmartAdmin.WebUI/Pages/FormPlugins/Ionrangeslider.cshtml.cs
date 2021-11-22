using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.FormPlugins
{
    public class IonrangesliderModel : PageModel
    {
        private readonly ILogger<IonrangesliderModel> _logger;

        public IonrangesliderModel(ILogger<IonrangesliderModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
