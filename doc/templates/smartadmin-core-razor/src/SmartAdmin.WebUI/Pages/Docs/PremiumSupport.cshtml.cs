using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Docs
{
    public class PremiumSupportModel : PageModel
    {
        private readonly ILogger<PremiumSupportModel> _logger;

        public PremiumSupportModel(ILogger<PremiumSupportModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
