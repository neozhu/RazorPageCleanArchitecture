using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class ForumListModel : PageModel
    {
        private readonly ILogger<ForumListModel> _logger;

        public ForumListModel(ILogger<ForumListModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
