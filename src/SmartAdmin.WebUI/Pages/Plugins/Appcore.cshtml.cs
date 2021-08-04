using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Plugins
{
    public class AppcoreModel : PageModel
    {
        private readonly ILogger<AppcoreModel> _logger;

        public AppcoreModel(ILogger<AppcoreModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
