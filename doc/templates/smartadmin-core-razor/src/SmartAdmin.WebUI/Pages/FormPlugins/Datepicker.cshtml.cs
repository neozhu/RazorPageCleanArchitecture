using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.FormPlugins
{
    public class DatepickerModel : PageModel
    {
        private readonly ILogger<DatepickerModel> _logger;

        public DatepickerModel(ILogger<DatepickerModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
