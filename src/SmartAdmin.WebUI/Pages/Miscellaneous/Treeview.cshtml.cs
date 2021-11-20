using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Miscellaneous
{
    public class TreeviewModel : PageModel
    {
        private readonly ILogger<TreeviewModel> _logger;

        public TreeviewModel(ILogger<TreeviewModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
