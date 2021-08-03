using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Datatables
{
    public class FixedcolumnsModel : PageModel
    {
        private readonly ILogger<FixedcolumnsModel> _logger;

        public FixedcolumnsModel(ILogger<FixedcolumnsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
