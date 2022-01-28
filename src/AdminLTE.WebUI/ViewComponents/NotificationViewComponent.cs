using AdminLTE.WebUI.Pages.Shared.Components.Notification;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Interfaces.ToDo;
using CleanArchitecture.Razor.Application.Common.Interfaces.ToDo.DTOs;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace AdminLTE.WebUI.ViewComponents;

public class NotificationViewComponent : ViewComponent
{
    private readonly IToDoService _todo;

    public NotificationViewComponent(
       IToDoService todo
        )
    {
 
        _todo = todo;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {

        var data = await _todo.GetToDoList(CancellationToken.None);

        var model = new DefaultModel
        {
           List = data.ToList(),
           Count = data.Count()
        };
        return View(model);
    }

  
}
