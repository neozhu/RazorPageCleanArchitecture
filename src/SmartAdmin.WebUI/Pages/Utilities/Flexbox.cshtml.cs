using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class FlexboxModel : PageModel
    {
        private readonly ILogger<FlexboxModel> _logger;

        public FlexboxModel(ILogger<FlexboxModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
