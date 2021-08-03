using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Documentation
{
    public class SiteStructureModel : PageModel
    {
        private readonly ILogger<SiteStructureModel> _logger;

        public SiteStructureModel(ILogger<SiteStructureModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
