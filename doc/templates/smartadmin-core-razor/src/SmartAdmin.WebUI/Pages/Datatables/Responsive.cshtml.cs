using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Datatables
{
    public class ResponsiveModel : PageModel
    {
        private readonly ILogger<ResponsiveModel> _logger;

        public ResponsiveModel(ILogger<ResponsiveModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
