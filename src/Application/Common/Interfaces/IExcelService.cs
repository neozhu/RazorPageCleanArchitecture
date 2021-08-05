using CleanArchitecture.Razor.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.Common.Interfaces
{
    public interface IExcelService
    {
        Task<Stream> ExportAsync<TData>(IEnumerable<TData> data
            , Dictionary<string, Func<TData, object>> mappers
            , string sheetName = "Sheet1");

        Task<Result> ImportAsync<TEntity>(Stream data
            , Dictionary<string, Func<DataRow, TEntity, object>> mappers
            , string sheetName = "Sheet1");
    }

}