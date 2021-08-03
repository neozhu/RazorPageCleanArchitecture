using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Documentation
{
    public class LicensingInformationModel : PageModel
    {
        private readonly ILogger<LicensingInformationModel> _logger;

        public LicensingInformationModel(ILogger<LicensingInformationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
