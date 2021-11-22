using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Settings
{
    public class SavingDbModel : PageModel
    {
        private readonly ILogger<SavingDbModel> _logger;

        public SavingDbModel(ILogger<SavingDbModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
