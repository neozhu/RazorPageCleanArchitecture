using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Datatables
{
    public class SelectModel : PageModel
    {
        private readonly ILogger<SelectModel> _logger;

        public SelectModel(ILogger<SelectModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
