using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Statistics
{
    public class ChartjsModel : PageModel
    {
        private readonly ILogger<ChartjsModel> _logger;

        public ChartjsModel(ILogger<ChartjsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
