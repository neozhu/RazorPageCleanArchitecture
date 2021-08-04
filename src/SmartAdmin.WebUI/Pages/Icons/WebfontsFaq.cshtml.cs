using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Icons
{
    public class WebfontsFaqModel : PageModel
    {
        private readonly ILogger<WebfontsFaqModel> _logger;

        public WebfontsFaqModel(ILogger<WebfontsFaqModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
