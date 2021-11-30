using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AdminLTE.WebUI.Pages.Shared.Components.ProjectSelector;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using Microsoft.Extensions.Primitives;

namespace AdminLTE.WebUI.ViewComponents;

public class ProjectSelectorViewComponent : ViewComponent
{
    private readonly IApplicationDbContext _context;

    public ProjectSelectorViewComponent(
        IApplicationDbContext context
        )
    {
        _context = context;
    }
    public IViewComponentResult Invoke()
    {
        if(HttpContext.Request.Query.TryGetValue("setProjectName",out StringValues projectname)
         && HttpContext.Request.Query.TryGetValue("setProjectId", out StringValues projectid)
          )
        {

        }
        var model = new DefaultModel();
        model.Projects = _context.MigrationProjects.Select(x=>x.Name).ToList();
       
            var list = _context.MigrationProjects.Select(x => new { x.Id, x.Name }).ToList();
            model.DefaultProjectId = list.FirstOrDefault()?.Id ?? 0;
            model.DefaultProjectName = list.FirstOrDefault()?.Name;
            model.Projects = list.Select(x => x.Name).ToList();
            if (HttpContext.Request.Cookies.TryGetValue("SELECTEDPROJECTID", out string defaultprojectId))
            {
                model.DefaultProjectId = int.Parse(defaultprojectId);
            }
            if (HttpContext.Request.Cookies.TryGetValue("SELECTEDPROJECTNAME", out string  defaultprojectName))
            {
                model.DefaultProjectName = defaultprojectName;
            }
 
        return View(model);
    }
}
