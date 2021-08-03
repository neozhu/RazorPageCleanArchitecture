using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Datatables
{
    public class ColumnfilterModel : PageModel
    {
        private readonly ILogger<ColumnfilterModel> _logger;

        public ColumnfilterModel(ILogger<ColumnfilterModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
