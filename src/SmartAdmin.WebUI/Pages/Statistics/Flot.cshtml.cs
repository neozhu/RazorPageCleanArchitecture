using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Statistics
{
    public class FlotModel : PageModel
    {
        private readonly ILogger<FlotModel> _logger;

        public FlotModel(ILogger<FlotModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
