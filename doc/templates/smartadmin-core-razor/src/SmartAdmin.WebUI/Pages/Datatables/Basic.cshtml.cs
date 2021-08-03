using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Datatables
{
    public class BasicModel : PageModel
    {
        private readonly ILogger<BasicModel> _logger;

        public BasicModel(ILogger<BasicModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
