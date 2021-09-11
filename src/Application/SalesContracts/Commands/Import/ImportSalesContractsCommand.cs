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
using CleanArchitecture.Razor.Application.SalesContracts.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.SalesContracts.Commands.Import
{
    public class ImportSalesContractsCommand : IRequest<Result>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
    public class CreateSalesContractsTemplateCommand : IRequest<byte[]>
    {
        public IEnumerable<string> Fields { get; set; }
        public string SheetName { get; set; }
    }

    public class ImportSalesContractsCommandHandler :
                 IRequestHandler<CreateSalesContractsTemplateCommand, byte[]>,
                 IRequestHandler<ImportSalesContractsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportSalesContractsCommandHandler> _localizer;
        private readonly IExcelService _excelService;

        public ImportSalesContractsCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportSalesContractsCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        public async Task<Result> Handle(ImportSalesContractsCommand request, CancellationToken cancellationToken)
        {

            var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, SalesContractDto, object>>
            {
                { _localizer["Contract No"], (row,item) => item.ContractNo = row[_localizer["Contract No"]]?.ToString() },
                { _localizer["Description"], (row,item) => item.Description = row[_localizer["Description"]]?.ToString() },
                { _localizer["Project Name"], (row,item) => item.ProjectName = row[_localizer["Project Name"]]?.ToString() },
                { _localizer["Customer Name"], (row,item) => item.CustomerName = row[_localizer["Customer Name"]]?.ToString() },
                { _localizer["Contract Date"], (row,item) => item.ContractDate =row.IsNull(_localizer["Contract Date"])?DateTime.Now:DateTime.Parse(row[_localizer["Contract Date"]].ToString()) },
                { _localizer["Order No"], (row,item) => item.OrderNo = row[_localizer["Order No"]]?.ToString() },
                { _localizer["Contract Amount"], (row,item) => item.ContractAmount =row.IsNull(_localizer["Contract Amount"])?0m:decimal.Parse(row[_localizer["Contract Amount"]].ToString()) },
                { _localizer["Tax Rate"], (row,item) => item.TaxRate =row.IsNull(_localizer["Tax Rate"])?0.13m: decimal.Parse(row[_localizer["Tax Rate"]].ToString()) },
                { _localizer["Receipt Amount"], (row,item) => item.ReceiptAmount = row.IsNull(_localizer["Receipt Amount"])?0.13m: decimal.Parse(row[_localizer["Receipt Amount"]].ToString()) },
                { _localizer["Invoice Amount"], (row,item) => item.InvoiceAmount = row.IsNull(_localizer["Invoice Amount"])?0.13m: decimal.Parse(row[_localizer["Invoice Amount"]].ToString()) },
                { _localizer["Balance"], (row,item) => item.Balance = row.IsNull(_localizer["Balance"])?0m: decimal.Parse(row[_localizer["Balance"]].ToString()) },
                { _localizer["Comments"], (row,item) => item.ContractNo = row[_localizer["Comments"]]?.ToString() },
           }, _localizer["SalesContracts"]);
            if (result.Succeeded)
            {
                var projectlist = new List<Project>();
                var customerlist = new List<Customer>();
                foreach (var item in result.Data)
                {

                    if (!(await _context.SalesContracts.AnyAsync(x => x.ContractNo == item.ContractNo)))
                    {
                        var project = projectlist.FirstOrDefault(x => x.Name == item.ProjectName);
                        if (project == null)
                        {
                            project = await _context.Projects.FirstOrDefaultAsync(x => x.Name == item.ProjectName);
                            projectlist.Add(project);
                        }
                        if (project == null)
                        {
                            project = new Project() { Name = item.ProjectName, Description = "从销售合同导入" };
                            _context.Projects.Add(project);
                            projectlist.Add(project);
                        }
                        var customer = customerlist.FirstOrDefault(x => x.Name == item.CustomerName);
                        if (customer == null)
                        {
                            customer = await _context.Customers.FirstOrDefaultAsync(x => x.Name == item.CustomerName);
                            customerlist.Add(customer);
                        }
                        if (customer == null)
                        {
                            customer = new Customer()
                            {
                                Name = item.CustomerName,
                                PartnerType = Domain.Enums.PartnerType.Customer,
                                Comments = "从销售合同导入",
                            };
                            _context.Customers.Add(customer);
                            customerlist.Add(customer);
                        }
                        var salesContract = new SalesContract()
                        {

                            ClosedDate = item.ClosedDate,
                            Comments = item.Comments,
                            CustomerId = customer.Id,
                            Customer = customer,
                            Description = item.Description,
                            Status = "Draft",
                            ContractAmount = item.ContractAmount,
                            ContractDate = item.ContractDate,
                            ContractNo = item.ContractNo,
                            InvoiceAmount = item.InvoiceAmount,
                            OrderNo = item.OrderNo,
                            Project = project,
                            ProjectId = project.Id,
                            ReceiptAmount = item.ReceiptAmount,
                            ContractIncAmount = item.ContractIncAmount,
                            TaxRate = item.TaxRate,
                            Balance = item.ContractAmount - item.ReceiptAmount
                        };
                        _context.SalesContracts.Add(salesContract);
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
        public async Task<byte[]> Handle(CreateSalesContractsTemplateCommand request, CancellationToken cancellationToken)
        {
            var fields = new string[] {
                  _localizer["Contract No"],
                  _localizer["Description"],
                  _localizer["Project Name"],
                  _localizer["Customer Name"],
                  _localizer["Contract Date"],
                  _localizer["Order No"],
                  _localizer["Contract Amount"],
                  _localizer["Tax Rate"],
                  _localizer["Receipt Amount"],
                  _localizer["Invoice Amount"],
                  _localizer["Balance"],
                  _localizer["Comments"],
                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer["SalesContracts"]);
            return result;
        }
    }
}
