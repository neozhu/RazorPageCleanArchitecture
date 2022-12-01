using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class InvoiceModel : PageModel
    {
        private readonly ILogger<InvoiceModel> _logger;

        public InvoiceModel(ILogger<InvoiceModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
