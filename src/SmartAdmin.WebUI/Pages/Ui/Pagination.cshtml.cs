using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class PaginationModel : PageModel
    {
        private readonly ILogger<PaginationModel> _logger;

        public PaginationModel(ILogger<PaginationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
