using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AdminLTE.WebUI.Pages.Shared.Components.ProjectSelector;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using Microsoft.Extensions.Primitives;
using CleanArchitecture.Razor.Application.MigrationProjects.DTOs;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace AdminLTE.WebUI.ViewComponents;

public class ProjectSelectorViewComponent : ViewComponent
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProjectSelectorViewComponent(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public IViewComponentResult Invoke()
    {
        if(Request.Query.TryGetValue("projectId",out StringValues projectid)
            && Request.Query.TryGetValue("projectName", out StringValues projectname)){
            var projectId = projectid.First();
            var projectName=projectname.First();
            HttpContext.Response.Cookies.Append("SELECTEDPROJECTID", projectId);
            HttpContext.Response.Cookies.Append("SELECTEDPROJECTNAME", projectName);
        }
        var model = new DefaultModel();
        model.Projects = _context.MigrationProjects.ProjectTo<MigrationProjectDto>(_mapper.ConfigurationProvider).ToList();
        return View(model);
    }
}
