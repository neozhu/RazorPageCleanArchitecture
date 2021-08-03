using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class PaginationModel : PageModel
    {
        private readonly ILogger<PaginationModel> _logger;

        public PaginationModel(ILogger<PaginationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
