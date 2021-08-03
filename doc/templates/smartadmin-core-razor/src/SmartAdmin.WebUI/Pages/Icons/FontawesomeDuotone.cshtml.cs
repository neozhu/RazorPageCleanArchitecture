using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Icons
{
    public class FontawesomeDuotoneModel : PageModel
    {
        private readonly ILogger<FontawesomeDuotoneModel> _logger;

        public FontawesomeDuotoneModel(ILogger<FontawesomeDuotoneModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
