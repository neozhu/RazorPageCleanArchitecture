using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Statistics
{
    public class ChartistModel : PageModel
    {
        private readonly ILogger<ChartistModel> _logger;

        public ChartistModel(ILogger<ChartistModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
