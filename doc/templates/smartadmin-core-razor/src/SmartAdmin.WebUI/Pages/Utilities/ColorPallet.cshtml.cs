using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class ColorPalletModel : PageModel
    {
        private readonly ILogger<ColorPalletModel> _logger;

        public ColorPalletModel(ILogger<ColorPalletModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
