using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.InvoiceDetails.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.InvoiceDetails.Commands.Import
{
    public class ImportInvoiceDetailsCommand: IRequest<Result>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
    public class CreateInvoiceDetailsTemplateCommand : IRequest<byte[]>
    {
        public IEnumerable<string> Fields { get; set; }
        public string SheetName { get; set; }
    }

    public class ImportInvoiceDetailsCommandHandler : 
                 IRequestHandler<CreateInvoiceDetailsTemplateCommand, byte[]>,
                 IRequestHandler<ImportInvoiceDetailsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportInvoiceDetailsCommandHandler> _localizer;
        private readonly IExcelService _excelService;

        public ImportInvoiceDetailsCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportInvoiceDetailsCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        public async Task<Result> Handle(ImportInvoiceDetailsCommand request, CancellationToken cancellationToken)
        {
          
           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, InvoiceDetailDto, object>>
            {
                { _localizer["Contract No"], (row,item) => item.ContractNo = row[_localizer["ContractNo"]].ToString() },
                { _localizer["Invoice No"], (row,item) => item.InvoiceNo = row[_localizer["Invoice No"]].ToString() },
                { _localizer["Invoice Date"], (row,item) => item.InvoiceDate =row.IsNull(_localizer["Invoice Date"])?DateTime.Now:DateTime.Parse(row[_localizer["Invoice Date"]].ToString()) },
                { _localizer["Due Time"], (row,item) => item.DueTime = row.IsNull(_localizer["Due Time"])?DateTime.Now.AddMonths(3):DateTime.Parse(row[_localizer["Due Time"]].ToString()) },
                { _localizer["Tax Rate"], (row,item) => item.TaxRate = row.IsNull(_localizer["Tax Rate"])?0.13m:decimal.Parse(row[_localizer["Tax Rate"]].ToString()) },
                { _localizer["Invoice Amount"], (row,item) => item.InvoiceAmount = row.IsNull(_localizer["Invoice Amount"])?0m:decimal.Parse(row[_localizer["Invoice Amount"]].ToString()) },
                { _localizer["Has Paid"], (row,item) => item.HasPaid =row.IsNull(_localizer["Has Paid"])?false:bool.Parse(row[_localizer["Has Paid"]].ToString()) },
                { _localizer["Comments"], (row,item) => item.Comments = row[_localizer["Comments"]].ToString() },

            }, _localizer["InvoiceDetails"]);
            if (result.Succeeded)
            {
             
                foreach (var item in result.Data)
                {
                    if (!(await _context.InvoiceDetails.AnyAsync(x => x.InvoiceNo == item.InvoiceNo)))
                    {
                        var contract = await _context.SalesContracts.FirstAsync(x => x.ContractNo==item.ContractNo);
                        if (contract == null)
                        {
                            return Result.Failure(new string[] {"没有找到销售合同" });
                        }
                         
                        
                        var invoice = new InvoiceDetail()
                        {
                            Balance = item.Balance,
                            Comments = item.Comments,
                            ContractAmount = contract.ContractAmount,
                            DueTime = item.DueTime,
                            InvoiceAmount = item.InvoiceAmount,
                            InvoiceDate = item.InvoiceDate,
                            HasPaid = item.HasPaid,
                            InvoiceNo = item.InvoiceNo,
                            SalesContractId = contract.Id,
                            TaxRate = item.TaxRate,

                        };
                        _context.InvoiceDetails.Add(invoice);
                    }
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return Result.Success();
            }
            else
            {
                return await Result.FailureAsync(result.Errors);
            }
        }
        public async Task<byte[]> Handle(CreateInvoiceDetailsTemplateCommand request, CancellationToken cancellationToken)
        {
            
            var fields = new string[] {
                  _localizer["Contract No"],
                  _localizer["Invoice No"],
                  _localizer["Invoice Date"],
                  _localizer["Due Time"],
                  _localizer["Tax Rate"],
                  _localizer["Invoice Amount"],
                  _localizer["Has Paid"],
                  _localizer["Comments"]
              
                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer["InvoiceDetails"]);
            return result;
        }
    }
}
