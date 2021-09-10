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
using CleanArchitecture.Razor.Application.SalesContractDetails.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.SalesContractDetails.Commands.Import
{
    public class ImportSalesContractDetailsCommand: IRequest<Result>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
    public class CreateSalesContractDetailsTemplateCommand : IRequest<byte[]>
    {
        public IEnumerable<string> Fields { get; set; }
        public string SheetName { get; set; }
    }

    public class ImportSalesContractDetailsCommandHandler : 
                 IRequestHandler<CreateSalesContractDetailsTemplateCommand, byte[]>,
                 IRequestHandler<ImportSalesContractDetailsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportSalesContractDetailsCommandHandler> _localizer;
        private readonly IExcelService _excelService;

        public ImportSalesContractDetailsCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportSalesContractDetailsCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        public async Task<Result> Handle(ImportSalesContractDetailsCommand request, CancellationToken cancellationToken)
        {
         
           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, SalesContractDetail, object>>
            {
                //ex. { _localizer["Name"], (row,item) => item.Name = row[_localizer["Name"]]?.ToString() },

            }, _localizer["SalesContractDetails"]);
           throw new System.NotImplementedException();
        }
        public async Task<byte[]> Handle(CreateSalesContractDetailsTemplateCommand request, CancellationToken cancellationToken)
        {
           
            var fields = new string[] {
                   //_localizer["Name"],
                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer["SalesContractDetails"]);
            return result;
        }
    }
}
