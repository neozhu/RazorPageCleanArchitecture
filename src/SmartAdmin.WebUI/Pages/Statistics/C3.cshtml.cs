using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Statistics
{
    public class C3Model : PageModel
    {
        private readonly ILogger<C3Model> _logger;

        public C3Model(ILogger<C3Model> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
