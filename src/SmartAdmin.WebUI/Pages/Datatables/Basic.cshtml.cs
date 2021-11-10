using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Datatables
{
    public class BasicModel : PageModel
    {
        private readonly ILogger<BasicModel> _logger;

        public BasicModel(ILogger<BasicModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
