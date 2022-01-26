using AdminLTE.WebUI.Pages.Shared.Components.Notification;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Interfaces.ToDo.DTOs;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace AdminLTE.WebUI.ViewComponents;

public class NotificationViewComponent : ViewComponent
{
    private readonly ICurrentUserService _currentUser;
    private readonly IApplicationDbContext _context;

    public NotificationViewComponent(
        ICurrentUserService currentUser,
      IApplicationDbContext context
        )
    {
        _currentUser = currentUser;
        _context = context;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var currentuser = _currentUser.UserId;
        var data =await _context.ToDoItems.Where(x =>
        (x.MappingRule.CreatedBy == currentuser || x.LastModifiedBy == currentuser)
        && x.IsDone==false)
            .Include(x => x.MappingRule).Include(x => x.ResultMapping)
            .OrderByDescending(x=>x.Id)
            .Select(x => new ToDoItemDto()
            {
                Created = x.Created,
                CreatedBy = x.CreatedBy,
                TimeAgo= GetTimeSince(x.Created),
                Title = x.Title,
                Name = (x.MappingRule != null ? x.MappingRule.Name : (x.ResultMapping != null ? x.ResultMapping.Name : null))
            }).ToListAsync();
            ;

        var model = new DefaultModel
        {
           List = data,
           Count = data.Count
        };
        return View(model);
    }

    public static string GetTimeSince(DateTime objDateTime)
    {
        // here we are going to subtract the passed in DateTime from the current time converted to UTC
        TimeSpan ts = DateTime.Now.Subtract(objDateTime);
        int intDays = ts.Days;
        int intHours = ts.Hours;
        int intMinutes = ts.Minutes;
        int intSeconds = ts.Seconds;

        if (intDays > 0)
            return string.Format("{0} days", intDays);

        if (intHours > 0)
            return string.Format("{0} hours", intHours);

        if (intMinutes > 0)
            return string.Format("{0} minutes", intMinutes);

        if (intSeconds > 0)
            return string.Format("{0} seconds", intSeconds);

        // let's handle future times..just in case
        if (intDays < 0)
            return string.Format("in {0} days", Math.Abs(intDays));

        if (intHours < 0)
            return string.Format("in {0} hours", Math.Abs(intHours));

        if (intMinutes < 0)
            return string.Format("in {0} minutes", Math.Abs(intMinutes));

        if (intSeconds < 0)
            return string.Format("just now", Math.Abs(intSeconds));

        return "a bit";
    }
}
