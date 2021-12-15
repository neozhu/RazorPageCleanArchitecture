using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.ResultMappings.DTOs;

public class StatusSummarizingDto
{
    public string Status { get; set; }
    public int Total { get; set; }
    public int Percentage { get; set; }
}
