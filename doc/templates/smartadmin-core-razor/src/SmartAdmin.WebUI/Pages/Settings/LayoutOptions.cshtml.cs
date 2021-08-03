using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Settings
{
    public class LayoutOptionsModel : PageModel
    {
        private readonly ILogger<LayoutOptionsModel> _logger;

        public LayoutOptionsModel(ILogger<LayoutOptionsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
