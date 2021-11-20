using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class ToastsModel : PageModel
    {
        private readonly ILogger<ToastsModel> _logger;

        public ToastsModel(ILogger<ToastsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
