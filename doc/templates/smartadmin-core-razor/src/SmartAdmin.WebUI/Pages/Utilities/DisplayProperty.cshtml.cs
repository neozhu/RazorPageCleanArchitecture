using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class DisplayPropertyModel : PageModel
    {
        private readonly ILogger<DisplayPropertyModel> _logger;

        public DisplayPropertyModel(ILogger<DisplayPropertyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
