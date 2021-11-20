using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class BreadcrumbsModel : PageModel
    {
        private readonly ILogger<BreadcrumbsModel> _logger;

        public BreadcrumbsModel(ILogger<BreadcrumbsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
