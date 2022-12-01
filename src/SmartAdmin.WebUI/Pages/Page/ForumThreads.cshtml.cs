using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class ForumThreadsModel : PageModel
    {
        private readonly ILogger<ForumThreadsModel> _logger;

        public ForumThreadsModel(ILogger<ForumThreadsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
