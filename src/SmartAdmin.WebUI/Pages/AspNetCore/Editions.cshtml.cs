using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.AspNetCore
{
    public class EditionsModel : PageModel
    {
        private readonly ILogger<EditionsModel> _logger;

        public EditionsModel(ILogger<EditionsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
