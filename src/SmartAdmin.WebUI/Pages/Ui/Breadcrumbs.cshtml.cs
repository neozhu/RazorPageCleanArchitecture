using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class BreadcrumbsModel : PageModel
    {
        private readonly ILogger<BreadcrumbsModel> _logger;

        public BreadcrumbsModel(ILogger<BreadcrumbsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
