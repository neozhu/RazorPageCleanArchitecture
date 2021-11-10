using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Icons
{
    public class StackGenerateModel : PageModel
    {
        private readonly ILogger<StackGenerateModel> _logger;

        public StackGenerateModel(ILogger<StackGenerateModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
