using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Statistics
{
    public class SparklineModel : PageModel
    {
        private readonly ILogger<SparklineModel> _logger;

        public SparklineModel(ILogger<SparklineModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
