using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Intel
{
    public class AnalyticsDashboardModel : PageModel
    {
        private readonly ILogger<AnalyticsDashboardModel> _logger;

        public AnalyticsDashboardModel(ILogger<AnalyticsDashboardModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
