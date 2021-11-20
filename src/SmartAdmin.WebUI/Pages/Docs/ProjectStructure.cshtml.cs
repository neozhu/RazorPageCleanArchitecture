using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Docs
{
    public class ProjectStructureModel : PageModel
    {
        private readonly ILogger<ProjectStructureModel> _logger;

        public ProjectStructureModel(ILogger<ProjectStructureModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
