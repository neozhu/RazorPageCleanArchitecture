using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class TypographyModel : PageModel
    {
        private readonly ILogger<TypographyModel> _logger;

        public TypographyModel(ILogger<TypographyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
