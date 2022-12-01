using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class ForumDiscussionModel : PageModel
    {
        private readonly ILogger<ForumDiscussionModel> _logger;

        public ForumDiscussionModel(ILogger<ForumDiscussionModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
