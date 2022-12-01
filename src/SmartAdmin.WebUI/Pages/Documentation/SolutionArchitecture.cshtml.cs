using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Documentation
{
    public class SolutionArchitectureModel : PageModel
    {
        private readonly ILogger<SolutionArchitectureModel> _logger;

        public SolutionArchitectureModel(ILogger<SolutionArchitectureModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
