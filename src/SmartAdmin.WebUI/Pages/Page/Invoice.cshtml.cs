using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class InvoiceModel : PageModel
    {
        private readonly ILogger<InvoiceModel> _logger;

        public InvoiceModel(ILogger<InvoiceModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
