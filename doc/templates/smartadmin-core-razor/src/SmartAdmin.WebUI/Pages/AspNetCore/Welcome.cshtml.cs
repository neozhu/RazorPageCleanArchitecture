using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.AspNetCore
{
    public class WelcomeModel : PageModel
    {
        private readonly ILogger<WelcomeModel> _logger;

        public WelcomeModel(ILogger<WelcomeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
