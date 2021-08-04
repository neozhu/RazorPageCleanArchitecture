using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Settings
{
    public class HowItWorksModel : PageModel
    {
        private readonly ILogger<HowItWorksModel> _logger;

        public HowItWorksModel(ILogger<HowItWorksModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
