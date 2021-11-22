using Microsoft.AspNetCore.Mvc.RazorPages;

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
