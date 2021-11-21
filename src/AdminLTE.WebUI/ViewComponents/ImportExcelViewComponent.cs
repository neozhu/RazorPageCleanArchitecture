using Microsoft.AspNetCore.Mvc;
using AdminLTE.WebUI.Pages.Shared.Components.ImportExcel;

namespace AdminLTE.WebUI.ViewComponents;

public class ImportExcelViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string importUri, string getTemplateUri, string onImportedSucceeded)
    {
        return View(new DefaultModel()
        {
            ImportUri = importUri,
            GetTemplateUri = getTemplateUri,
            OnImportedSucceeded = onImportedSucceeded
        });
    }
}
