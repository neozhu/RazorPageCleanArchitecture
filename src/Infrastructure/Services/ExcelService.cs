using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using System.IO;
using System.Data;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using ClosedXML.Excel;
using CleanArchitecture.Razor.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CleanArchitecture.Razor.Infrastructure.Services
{
  public class ExcelService : IExcelService
  {
    private readonly IStringLocalizer<ExcelService> _localizer;

    public ExcelService(IStringLocalizer<ExcelService> localizer)
    {
      _localizer = localizer;
    }

    public async Task<Stream> ExportAsync<TData>(IEnumerable<TData> data
        , Dictionary<string, Func<TData, object>> mappers
        , string sheetName = "Sheet1")
    {
      using (var workbook = new XLWorkbook())
      {
        workbook.Properties.Author = "";
        var ws = workbook.Worksheets.Add(sheetName);
        var colIndex = 1;
        var rowIndex = 1;
        var headers = mappers.Keys.Select(x => x).ToList();
        foreach (var header in headers)
        {
          var cell = ws.Cell(rowIndex, colIndex);
          var fill = cell.Style.Fill;
          fill.PatternType = XLFillPatternValues.Solid;
          fill.SetBackgroundColor(XLColor.LightBlue);
          var border = cell.Style.Border;
          border.BottomBorder =
              border.BottomBorder =
                  border.BottomBorder =
                      border.BottomBorder = XLBorderStyleValues.Thin;

          cell.Value = header;

          colIndex++;
        }
        var dataList = data.ToList();
        foreach (var item in dataList)
        {
          colIndex = 1;
          rowIndex++;

          var result = headers.Select(header => mappers[header](item));

          foreach (var value in result)
          {
            ws.Cell(rowIndex, colIndex++).Value = value;
          }
        }
        using (var stream = new MemoryStream())
        {
          workbook.SaveAs(stream);
          return await Task.FromResult(stream);
        }
      }
    }

    public async Task<Result> ImportAsync<TEntity>(Stream stream, Dictionary<string, Func<DataRow, TEntity, object>> mappers, string sheetName = "Sheet1")
    {
      using(var workbook = new XLWorkbook(stream))
      {
        var ws = workbook.Worksheet(sheetName);
        if (ws == null)
        {
          return Result.Failure(new string[]{ string.Format(_localizer["Sheet with name {0} does not exist!"], sheetName)});
        }
        var dt = new DataTable();
        var titlesInFirstRow = true;

        foreach (var firstRowCell in ws.Range(1, 1, 1, ws.LastCellUsed().Address.ColumnNumber).Cells())
        {
          dt.Columns.Add(titlesInFirstRow ? firstRowCell.GetString() : $"Column {firstRowCell.Address.ColumnNumber}");
        }
        var startRow = titlesInFirstRow ? 2 : 1;
        var headers = mappers.Keys.Select(x => x).ToList();
        var errors = new List<string>();
        foreach (var header in headers)
        {
          if (!dt.Columns.Contains(header))
          {
            errors.Add(string.Format(_localizer["Header '{0}' does not exist in table!"], header));
          }
        }
        if (errors.Any())
        {
          return await Task.FromResult(Result.Failure(errors));
        }
        var lastrow = ws.LastRowUsed();
        foreach (IXLRow row in ws.Rows(startRow, lastrow.RowNumber()))
        {
          try
          {
            DataRow datarow = dt.Rows.Add();
            var item = (TEntity)Activator.CreateInstance(typeof(TEntity));
            foreach (IXLCell cell in row.Cells())
            {
              datarow[cell.Address.ColumnNumber - 1] = cell.ToString();
            }
            headers.ForEach(x => mappers[x](datarow, item));
          }catch(Exception e)
          {
            return await Task.FromResult(Result.Failure(new string[] { string.Format(_localizer["Sheet name {0}:{1}"], sheetName,e.Message) }));
          }
        }


        return await Task.FromResult(Result.Success());
      }
    }
  }
}