using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class HelpersModel : PageModel
    {
        private readonly ILogger<HelpersModel> _logger;

        public HelpersModel(ILogger<HelpersModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
