using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Datatables
{
    public class BasicModel : PageModel
    {
        private readonly ILogger<BasicModel> _logger;

        public BasicModel(ILogger<BasicModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
