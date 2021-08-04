using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Datatables
{
    public class RowgroupModel : PageModel
    {
        private readonly ILogger<RowgroupModel> _logger;

        public RowgroupModel(ILogger<RowgroupModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
