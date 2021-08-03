using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Plugins
{
    public class NavigationModel : PageModel
    {
        private readonly ILogger<NavigationModel> _logger;

        public NavigationModel(ILogger<NavigationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
