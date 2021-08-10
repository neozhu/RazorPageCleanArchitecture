using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartAdmin.WebUI.Models;
using SmartAdmin.WebUI.Pages.Shared.Components.ImportExcel;

namespace SmartAdmin.WebUI.ViewComponents
{
    public class ImportExcelViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string importUri,string onImportedSucceeded)
        {
            return View(new DefaultModel() { ImportUri = importUri , OnImportedSucceeded= onImportedSucceeded });
        }
    }
}
