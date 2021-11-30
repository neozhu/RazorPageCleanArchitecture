using System.Globalization;
using CleanArchitecture.Razor.Application.MigrationProjects.DTOs;

namespace AdminLTE.WebUI.Pages.Shared.Components.ProjectSelector;

public class DefaultModel
{
    public int DefaultProjectId { get; set; }
    public string DefaultProjectName { get; set;}
    public List<MigrationProjectDto> Projects { get; set; }
}
