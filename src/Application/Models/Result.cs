using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.Common.Models
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static Result Success()
        {
            return new Result(true, new string[] { });
        }
    public static Task<Result> SuccessAsync()
    {
      return Task.FromResult(new Result(true, new string[] { }));
    }
    public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    public static Task<Result> FailureAsync(IEnumerable<string> errors)
    {
      return Task.FromResult(new Result(false, errors));
    }
  }
}
