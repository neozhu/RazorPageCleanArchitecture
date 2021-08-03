using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Intel
{
    public class MarketingDashboardModel : PageModel
    {
        private readonly ILogger<MarketingDashboardModel> _logger;

        public MarketingDashboardModel(ILogger<MarketingDashboardModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
