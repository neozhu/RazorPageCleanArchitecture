using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.Models
{
  public class PaginatedData<T>
  {
    public int total { get; set; }
    public IEnumerable<T> rows { get; set; }
    public PaginatedData(IEnumerable<T> items, int total)
    {
      this.rows = items;
      this.total = total;
    }
  }
}
