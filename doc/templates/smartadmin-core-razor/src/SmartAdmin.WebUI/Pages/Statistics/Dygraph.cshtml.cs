using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Statistics
{
    public class DygraphModel : PageModel
    {
        private readonly ILogger<DygraphModel> _logger;

        public DygraphModel(ILogger<DygraphModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
