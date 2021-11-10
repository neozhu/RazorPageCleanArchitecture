using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class CarouselModel : PageModel
    {
        private readonly ILogger<CarouselModel> _logger;

        public CarouselModel(ILogger<CarouselModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
