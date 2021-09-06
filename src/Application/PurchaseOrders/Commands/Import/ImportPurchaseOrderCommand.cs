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
    public class ImportPurchaseOrdersCommand: IRequest<Result>
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
           throw new System.NotImplementedException();
        }
        public async Task<byte[]> Handle(CreatePurchaseOrdersTemplateCommand request, CancellationToken cancellationToken)
        {
            //TODO:Implementing ImportPurchaseOrdersCommandHandler method 
            var fields = new string[] {
      
                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer["PurchaseOrders"]);
            return result;
        }
    }
}
