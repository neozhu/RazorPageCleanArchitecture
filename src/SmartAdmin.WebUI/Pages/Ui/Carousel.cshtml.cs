using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class CarouselModel : PageModel
    {
        private readonly ILogger<CarouselModel> _logger;

        public CarouselModel(ILogger<CarouselModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
