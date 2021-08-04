using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class TabsPillsModel : PageModel
    {
        private readonly ILogger<TabsPillsModel> _logger;

        public TabsPillsModel(ILogger<TabsPillsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
