using Microsoft.AspNetCore.Mvc;
using SmartAdmin.WebUI.Pages.Shared.Components.ImportExcel;

namespace SmartAdmin.WebUI.ViewComponents
{
    public class ImportExcelViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string importUri,string getTemplateUri,string onImportedSucceeded)
        {
            return View(new DefaultModel() { ImportUri = importUri ,
                GetTemplateUri = getTemplateUri,
                OnImportedSucceeded= onImportedSucceeded });
        }
    }
}
