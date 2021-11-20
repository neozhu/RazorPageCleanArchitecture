using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class DropdownsModel : PageModel
    {
        private readonly ILogger<DropdownsModel> _logger;

        public DropdownsModel(ILogger<DropdownsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
