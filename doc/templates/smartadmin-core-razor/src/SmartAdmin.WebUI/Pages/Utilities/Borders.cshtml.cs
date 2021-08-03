using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class BordersModel : PageModel
    {
        private readonly ILogger<BordersModel> _logger;

        public BordersModel(ILogger<BordersModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
