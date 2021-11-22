using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Datatables
{
    public class FixedcolumnsModel : PageModel
    {
        private readonly ILogger<FixedcolumnsModel> _logger;

        public FixedcolumnsModel(ILogger<FixedcolumnsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
