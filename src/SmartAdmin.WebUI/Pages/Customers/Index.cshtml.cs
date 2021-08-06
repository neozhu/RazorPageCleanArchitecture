using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Customers.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace SmartAdmin.WebUI.Pages.Customers
{
    public class IndexModel : PageModel
    {
        public async Task OnGetAsync()
        {
        }
        public async Task<IActionResult> OnGetDataAsync([FromQuery] CustomersWithPaginationQuery command)
        {
            return new JsonResult("");
        }
    }
}
