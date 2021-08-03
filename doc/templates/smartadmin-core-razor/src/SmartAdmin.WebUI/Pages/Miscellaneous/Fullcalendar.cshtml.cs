using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Miscellaneous
{
    public class FullcalendarModel : PageModel
    {
        private readonly ILogger<FullcalendarModel> _logger;

        public FullcalendarModel(ILogger<FullcalendarModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
