using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class CardsModel : PageModel
    {
        private readonly ILogger<CardsModel> _logger;

        public CardsModel(ILogger<CardsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
