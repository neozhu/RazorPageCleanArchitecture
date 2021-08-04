using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Icons
{
    public class FontawesomeRegularModel : PageModel
    {
        private readonly ILogger<FontawesomeRegularModel> _logger;

        public FontawesomeRegularModel(ILogger<FontawesomeRegularModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
