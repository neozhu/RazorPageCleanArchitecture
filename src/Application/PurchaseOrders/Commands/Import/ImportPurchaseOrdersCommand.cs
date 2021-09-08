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
using CleanArchitecture.Razor.Application.PurchaseOrders.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.PurchaseOrders.Commands.Import
{
    public class ImportPurchaseOrdersCommand : IRequest<Result>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
    public class CreatePurchaseOrdersTemplateCommand : IRequest<byte[]>
    {
        public IEnumerable<string> Fields { get; set; }
        public string SheetName { get; set; }
    }

    public class ImportPurchaseOrdersCommandHandler :
                 IRequestHandler<CreatePurchaseOrdersTemplateCommand, byte[]>,
                 IRequestHandler<ImportPurchaseOrdersCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportPurchaseOrdersCommandHandler> _localizer;
        private readonly IExcelService _excelService;

        public ImportPurchaseOrdersCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportPurchaseOrdersCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        public async Task<Result> Handle(ImportPurchaseOrdersCommand request, CancellationToken cancellationToken)
        {
            //TODO:Implementing ImportPurchaseOrdersCommandHandler method
            var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, PurchaseOrderDto, object>>
            {
                { _localizer["PO"], (row,item) => item.PO = row[_localizer["PO"]]?.ToString() },
                { _localizer["Status"], (row,item) => item.Status = row[_localizer["Status"]]?.ToString() },
                { _localizer["Product Name"], (row,item) => item.ProductName = row[_localizer["Product Name"]]?.ToString() },
                { _localizer["Customer Name"], (row,item) => item.CustomerName = row[_localizer["Customer Name"]]?.ToString() },
                { _localizer["Description"], (row,item) => item.Description = row[_localizer["Description"]]?.ToString() },
                { _localizer["Order Date"], (row,item) => item.OrderDate = DateTime.Parse(row[_localizer["Order Date"]].ToString()) },
                { _localizer["Qty"], (row,item) => item.Qty = row.IsNull(_localizer["Qty"])?null:int.Parse(row[_localizer["Qty"]].ToString()) },
                { _localizer["Price"], (row,item) => item.Price = row.IsNull(_localizer["Price"])?null:decimal.Parse(row[_localizer["Price"]].ToString()) },
                { _localizer["Amount"], (row,item) => item.Amount = row.IsNull(_localizer["Amount"])?0m:decimal.Parse(row[_localizer["Amount"]].ToString()) },
                { _localizer["Invoice No"], (row,item) => item.InvoiceNo = row[_localizer["Invoice No"]]?.ToString() },
                { _localizer["Tax Rate"], (row,item) => item.TaxRate = row.IsNull(_localizer["Tax Rate"])?null:decimal.Parse(row[_localizer["Tax Rate"]].ToString()) },
                { _localizer["Is Special"], (row,item) => item.IsSpecial =bool.Parse(row[_localizer["Is Special"]].ToString()) }

            }, _localizer["PurchaseOrders"]);
            if (result.Succeeded)
            {
                var customerlist=new List<Customer>();
                var productlist=new List<Product>();
                foreach (var item in result.Data)
                {
                    if (!(await _context.PurchaseOrders.AnyAsync(x => x.PO == item.PO)))
                    {
                        var product = productlist.FirstOrDefault(x => x.Name == item.ProductName);
                        if (product != null)
                        {
                            product = await _context.Products.FirstOrDefaultAsync(x => x.Name == item.ProductName);
                            productlist.Add(product);
                            }
                        if (product == null)
                        {
                            product = new Product() { Name = item.ProductName, Description = "从采购单导入" };
                            _context.Products.Add(product);
                            productlist.Add(product);
                        }
                        var customer = customerlist.FirstOrDefault(x => x.Name == item.CustomerName);
                        if (customer != null)
                        {
                            customer = await _context.Customers.FirstOrDefaultAsync(x => x.Name == item.CustomerName);
                            customerlist.Add(customer);
                        }
                 
                        if (customer == null)
                        {
                            customer = new Customer()
                            {
                                Name = item.CustomerName,
                                PartnerType = Domain.Enums.PartnerType.Supplier,
                                Comments = "从采购单导入",
                            };
                            _context.Customers.Add(customer);
                            customerlist.Add(customer);
                        }
                        var purchaseOrder = new PurchaseOrder()
                        {
                            PO = item.PO,
                            Amount = item.Amount,
                            CustomerId = customer.Id,
                            Customer = customer,
                            InvoiceNo = item.InvoiceNo,
                            IsSpecial = item.IsSpecial,
                            Description = item.Description,
                            OrderDate = item.OrderDate,
                            Product = product,
                            ProductId = product.Id,
                            Price = item.Price,
                            Qty = item.Qty,
                            Status = "Draft",
                            TaxRate = item.TaxRate,

                        };
                        _context.PurchaseOrders.Add(purchaseOrder);
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
        public async Task<byte[]> Handle(CreatePurchaseOrdersTemplateCommand request, CancellationToken cancellationToken)
        {

            var fields = new string[] {
                _localizer["PO"],
                _localizer["Product Name"],
                _localizer["Customer Name"],
                _localizer["Description"],
                _localizer["Order Date"],
                _localizer["Qty"],
                _localizer["Price"],
                _localizer["Amount"],
                _localizer["Invoice No"],
                _localizer["Tax Rate"],
                _localizer["Is Special"]
                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer["PurchaseOrders"]);
            return result;
        }
    }
}
