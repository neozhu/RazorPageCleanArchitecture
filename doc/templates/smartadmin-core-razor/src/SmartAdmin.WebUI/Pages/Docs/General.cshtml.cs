using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Docs
{
    public class GeneralModel : PageModel
    {
        private readonly ILogger<GeneralModel> _logger;

        public GeneralModel(ILogger<GeneralModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
