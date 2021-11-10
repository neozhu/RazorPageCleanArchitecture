using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Datatables
{
    public class AutofillModel : PageModel
    {
        private readonly ILogger<AutofillModel> _logger;

        public AutofillModel(ILogger<AutofillModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
