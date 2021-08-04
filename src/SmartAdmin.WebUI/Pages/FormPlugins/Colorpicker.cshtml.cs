using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.FormPlugins
{
    public class ColorpickerModel : PageModel
    {
        private readonly ILogger<ColorpickerModel> _logger;

        public ColorpickerModel(ILogger<ColorpickerModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
