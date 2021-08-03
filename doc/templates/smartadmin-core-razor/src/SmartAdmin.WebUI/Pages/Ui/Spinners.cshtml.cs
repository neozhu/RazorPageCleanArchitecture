using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class SpinnersModel : PageModel
    {
        private readonly ILogger<SpinnersModel> _logger;

        public SpinnersModel(ILogger<SpinnersModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
