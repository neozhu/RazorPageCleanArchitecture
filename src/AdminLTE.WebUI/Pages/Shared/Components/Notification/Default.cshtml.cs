using System.Globalization;
using CleanArchitecture.Razor.Application.Common.Interfaces.ToDo.DTOs;

namespace AdminLTE.WebUI.Pages.Shared.Components.Notification;

public class DefaultModel
{

    public List<ToDoItemDto> List { get; set; } = new();
    public int Count { get; set; }
}
