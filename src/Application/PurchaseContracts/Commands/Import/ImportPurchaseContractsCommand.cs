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
using CleanArchitecture.Razor.Application.PurchaseContracts.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.PurchaseContracts.Commands.Import
{
    public class ImportPurchaseContractsCommand: IRequest<Result>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
    public class CreatePurchaseContractsTemplateCommand : IRequest<byte[]>
    {
        public IEnumerable<string> Fields { get; set; }
        public string SheetName { get; set; }
    }

    public class ImportPurchaseContractsCommandHandler : 
                 IRequestHandler<CreatePurchaseContractsTemplateCommand, byte[]>,
                 IRequestHandler<ImportPurchaseContractsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportPurchaseContractsCommandHandler> _localizer;
        private readonly IExcelService _excelService;

        public ImportPurchaseContractsCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportPurchaseContractsCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        public async Task<Result> Handle(ImportPurchaseContractsCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing ImportPurchaseContractsCommandHandler method
           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, PurchaseContractDto, object>>
            {
                { _localizer["Contract No"], (row,item) => item.ContractNo = row[_localizer["Contract No"]].ToString() },
                { _localizer["Description"], (row,item) => item.Description = row[_localizer["Description"]].ToString() },
                { _localizer["Project Name"], (row,item) => item.ProjectName = row[_localizer["Project Name"]].ToString() },
                { _localizer["Customer Name"], (row,item) => item.CustomerName = row[_localizer["Customer Name"]].ToString() },
                { _localizer["Contract Date"], (row,item) => item.ContractDate =DateTime.Parse(row[_localizer["ContractDate"]].ToString()) },
                { _localizer["Order No"], (row,item) => item.OrderNo = row[_localizer["Order No"]].ToString() },
                { _localizer["Contract Amount"], (row,item) => item.ContractAmount =decimal.Parse(row[_localizer["Contract Amount"]].ToString()) },
                { _localizer["Paid Amount"], (row,item) => item.PaidAmount = row.IsNull(_localizer["Paid Amount"])? 0m:decimal.Parse( row[_localizer["Paid Amount"]].ToString()) },
                { _localizer["Invoice Amount"], (row,item) => item.InvoiceAmount =row.IsNull(_localizer["Invoice Amount"])?0m: decimal.Parse(row[_localizer["Invoice Amount"]].ToString()) },
                { _localizer["Balance"], (row,item) => item.Balance =row.IsNull(_localizer["Balance"])?0m:decimal.Parse(row[_localizer["Balance"]].ToString()) },
                { _localizer["Comments"], (row,item) => item.Comments = row[_localizer["Comments"]].ToString() }
            

            }, _localizer["PurchaseContracts"]);
           throw new System.NotImplementedException();
        }
        public async Task<byte[]> Handle(CreatePurchaseContractsTemplateCommand request, CancellationToken cancellationToken)
        {
            var fields = new string[] {
                  _localizer["Contract No"],
                  _localizer["Description"],
                  _localizer["Project Name"],
                  _localizer["Customer Name"],
                  _localizer["Contract Date"],
                  _localizer["Order No"],
                  _localizer["Contract Amount"],
                  _localizer["Paid Amount"],
                  _localizer["Invoice Amount"],
                  _localizer["Balance"],
                  _localizer["Comments"]
                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer["PurchaseContracts"]);
            return result;
        }
    }
}
