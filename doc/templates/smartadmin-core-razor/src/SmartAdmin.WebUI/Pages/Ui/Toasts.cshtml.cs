using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class ToastsModel : PageModel
    {
        private readonly ILogger<ToastsModel> _logger;

        public ToastsModel(ILogger<ToastsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
