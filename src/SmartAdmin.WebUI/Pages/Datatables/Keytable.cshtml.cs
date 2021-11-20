using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Datatables
{
    public class KeytableModel : PageModel
    {
        private readonly ILogger<KeytableModel> _logger;

        public KeytableModel(ILogger<KeytableModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
