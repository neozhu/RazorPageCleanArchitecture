using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Datatables
{
    public class ColreorderModel : PageModel
    {
        private readonly ILogger<ColreorderModel> _logger;

        public ColreorderModel(ILogger<ColreorderModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
