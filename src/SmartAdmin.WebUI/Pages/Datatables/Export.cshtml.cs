using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Datatables
{
    public class ExportModel : PageModel
    {
        private readonly ILogger<ExportModel> _logger;

        public ExportModel(ILogger<ExportModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
