using System.Text.Json;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE.WebUI.ViewComponents;

public class ChartViewComponent : ViewComponent
{
    private readonly IApplicationDbContext _context;

    public ChartViewComponent(
        IApplicationDbContext context
        )
    {
        _context = context;
    }
    public IViewComponentResult Invoke()
    {
        var resultdata1 = new List<dataitem>();
        var resultdata2 = new List<dataitem>();
        var data1 = _context.MappingRules.Where(x => x.Team != null)
                 .GroupBy(x => new { x.Team, x.Status })
                 .Select(x => new { x.Key.Team, x.Key.Status, Total= x.Count() });
        var data2 = _context.ResultMappings.Where(x => x.Team != null)
                 .GroupBy(x => new { x.Team, x.Status })
                 .Select(x => new { x.Key.Team, x.Key.Status, Total = x.Count() });
        foreach(var item in data1)
        {
            if(item.Team.IndexOfAny(new char[] { ',' })>0)
            {
                var teamarray=item.Team.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach(var t in teamarray)
                {
                    var dataitem = resultdata1.Where(x => x.Status == item.Status && x.Team == t.Trim()).FirstOrDefault();
                    if (dataitem == null)
                    {
                        dataitem = new dataitem()
                        {
                            Team = t,
                            Status = item.Status,
                            Total = item.Total,
                            Percent = item.Total
                        };
                        resultdata1.Add(dataitem);
                    }
                    else
                    {
                        dataitem.Total += item.Total;
                        dataitem.Percent = dataitem.Total;
                    }
                }
            }
            else
            {
                var dataitem = resultdata1.Where(x => x.Status == item.Status && x.Team == item.Team.Trim()).FirstOrDefault();
                if (dataitem == null)
                {
                    dataitem = new dataitem()
                    {
                        Team = item.Team,
                        Status = item.Status,
                        Total = item.Total,
                        Percent = item.Total
                    };
                    resultdata1.Add(dataitem);
                }
                else
                {
                    dataitem.Total += item.Total;
                    dataitem.Percent = dataitem.Total;
                }
            }
        }
        foreach (var item in data2)
        {
            if (item.Team.IndexOfAny(new char[] { ',' }) > 0)
            {
                var teamarray = item.Team.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var t in teamarray)
                {
                    var dataitem = resultdata2.Where(x => x.Status == item.Status && x.Team == t.Trim()).FirstOrDefault();
                    if (dataitem == null)
                    {
                        dataitem = new dataitem()
                        {
                            Team = t,
                            Status = item.Status,
                            Total = item.Total,
                            Percent = item.Total
                        };
                        resultdata2.Add(dataitem);
                    }
                    else
                    {
                        dataitem.Total += item.Total;
                        dataitem.Percent = dataitem.Total;
                    }
                }
            }
            else
            {
                var dataitem = resultdata2.Where(x => x.Status == item.Status && x.Team == item.Team.Trim()).FirstOrDefault();
                if (dataitem == null)
                {
                    dataitem = new dataitem()
                    {
                        Team = item.Team,
                        Status = item.Status,
                        Total = item.Total,
                        Percent = item.Total
                    };
                    resultdata2.Add(dataitem);
                }
                else
                {
                    dataitem.Total += item.Total;
                    dataitem.Percent = dataitem.Total;
                }
            }
        }
        var resultstr1 = JsonSerializer.Serialize(resultdata1);
        var resultstr2 = JsonSerializer.Serialize(resultdata2);
        return View( new AdminLTE.WebUI.Pages.Shared.Components.Chart.DefaultModel() {ruledata= resultstr1,resultdata= resultstr2 });
    }

    internal class dataitem
    {
        public string Team { get; set; }
        public string Status { get; set; }
        public int Total { get; set; }
        public int Percent { get; set; }
    }
}
