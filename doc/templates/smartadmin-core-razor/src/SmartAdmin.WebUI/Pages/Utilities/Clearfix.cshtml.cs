using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class ClearfixModel : PageModel
    {
        private readonly ILogger<ClearfixModel> _logger;

        public ClearfixModel(ILogger<ClearfixModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
