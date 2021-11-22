using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.FormPlugins
{
    public class Select2Model : PageModel
    {
        private readonly ILogger<Select2Model> _logger;

        public Select2Model(ILogger<Select2Model> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
