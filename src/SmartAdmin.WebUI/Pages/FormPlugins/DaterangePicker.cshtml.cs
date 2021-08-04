using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.FormPlugins
{
    public class DaterangePickerModel : PageModel
    {
        private readonly ILogger<DaterangePickerModel> _logger;

        public DaterangePickerModel(ILogger<DaterangePickerModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
