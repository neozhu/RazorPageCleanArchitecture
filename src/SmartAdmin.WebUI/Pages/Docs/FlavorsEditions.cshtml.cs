using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Docs
{
    public class FlavorsEditionsModel : PageModel
    {
        private readonly ILogger<FlavorsEditionsModel> _logger;

        public FlavorsEditionsModel(ILogger<FlavorsEditionsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
