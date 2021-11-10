using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class CardsModel : PageModel
    {
        private readonly ILogger<CardsModel> _logger;

        public CardsModel(ILogger<CardsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
