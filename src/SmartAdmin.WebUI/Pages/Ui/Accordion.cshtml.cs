using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class AccordionModel : PageModel
    {
        private readonly ILogger<AccordionModel> _logger;

        public AccordionModel(ILogger<AccordionModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
