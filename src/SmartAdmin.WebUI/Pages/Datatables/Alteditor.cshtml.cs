using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Datatables
{
    public class AlteditorModel : PageModel
    {
        private readonly ILogger<AlteditorModel> _logger;

        public AlteditorModel(ILogger<AlteditorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
