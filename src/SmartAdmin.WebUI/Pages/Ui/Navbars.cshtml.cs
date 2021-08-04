using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class NavbarsModel : PageModel
    {
        private readonly ILogger<NavbarsModel> _logger;

        public NavbarsModel(ILogger<NavbarsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
