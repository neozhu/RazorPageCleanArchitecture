using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Icons
{
    public class FontawesomeSolidModel : PageModel
    {
        private readonly ILogger<FontawesomeSolidModel> _logger;

        public FontawesomeSolidModel(ILogger<FontawesomeSolidModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
