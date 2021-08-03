using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Statistics
{
    public class PeityModel : PageModel
    {
        private readonly ILogger<PeityModel> _logger;

        public PeityModel(ILogger<PeityModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
