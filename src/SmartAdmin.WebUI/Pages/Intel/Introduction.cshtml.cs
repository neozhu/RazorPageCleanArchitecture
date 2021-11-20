using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Intel
{
    public class IntroductionModel : PageModel
    {
        private readonly ILogger<IntroductionModel> _logger;

        public IntroductionModel(ILogger<IntroductionModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
