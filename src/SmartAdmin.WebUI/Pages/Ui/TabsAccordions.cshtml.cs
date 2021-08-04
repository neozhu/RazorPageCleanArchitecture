using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class TabsAccordionsModel : PageModel
    {
        private readonly ILogger<TabsAccordionsModel> _logger;

        public TabsAccordionsModel(ILogger<TabsAccordionsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
