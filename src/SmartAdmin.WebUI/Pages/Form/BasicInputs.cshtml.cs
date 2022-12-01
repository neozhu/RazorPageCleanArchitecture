using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Form
{
    public class BasicInputsModel : PageModel
    {
        private readonly ILogger<BasicInputsModel> _logger;

        public BasicInputsModel(ILogger<BasicInputsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
