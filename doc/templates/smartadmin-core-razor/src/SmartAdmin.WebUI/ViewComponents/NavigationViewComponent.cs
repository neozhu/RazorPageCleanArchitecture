using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartAdmin.WebUI.Models;

namespace SmartAdmin.WebUI.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var items = NavigationModel.Full;

            return View(items);
        }
    }
}
