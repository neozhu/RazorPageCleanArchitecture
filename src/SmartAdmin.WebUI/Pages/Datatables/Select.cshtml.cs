using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Datatables
{
    public class SelectModel : PageModel
    {
        private readonly ILogger<SelectModel> _logger;

        public SelectModel(ILogger<SelectModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
