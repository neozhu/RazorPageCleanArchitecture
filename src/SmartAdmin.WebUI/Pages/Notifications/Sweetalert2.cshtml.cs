using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Notifications
{
    public class Sweetalert2Model : PageModel
    {
        private readonly ILogger<Sweetalert2Model> _logger;

        public Sweetalert2Model(ILogger<Sweetalert2Model> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
