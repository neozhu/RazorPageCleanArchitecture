using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class PopoversModel : PageModel
    {
        private readonly ILogger<PopoversModel> _logger;

        public PopoversModel(ILogger<PopoversModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
