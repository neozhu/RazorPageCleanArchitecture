using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.Customers.Commands.AddEdit;
using CleanArchitecture.Razor.Application.Products.Commands.AddEdit;
using CleanArchitecture.Razor.Application.Products.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.Products.Commands.Import
{
    public class ImportProductsCommand: IRequest<Result>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
    public class CreateProductsTemplateCommand : IRequest<byte[]>
    {
        public IEnumerable<string> Fields { get; set; }
        public string SheetName { get; set; }
    }

    public class ImportProductsCommandHandler :
        IRequestHandler<CreateProductsTemplateCommand, byte[]>,
        IRequestHandler<ImportProductsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportProductsCommandHandler> _localizer;
        private readonly IExcelService _excelService;
        private readonly IValidator<AddEditProductCommand> _addValidator;

        public ImportProductsCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<ImportProductsCommandHandler> localizer,
            IExcelService excelService,
            IValidator<AddEditProductCommand> addValidator,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _addValidator = addValidator;
            _mapper = mapper;
        }
        public async Task<Result> Handle(ImportProductsCommand request, CancellationToken cancellationToken)
        {
            //TODO:Implementing ImportProductCommandHandler method 
            var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, Product, object>>
            {
                { _localizer["Name"], (row,item) => item.Name = row[_localizer["Name"]]?.ToString() },
                { _localizer["Description"], (row,item) => item.Description =  row[_localizer["Description"]]?.ToString() }
            }, _localizer["Products"]);

            if (result.Succeeded)
            {
                var importItems = result.Data;
                var errors = new List<string>();
                var errorsOccurred = false;
                foreach (var item in importItems)
                {
                    var validationResult = await _addValidator.ValidateAsync(_mapper.Map<AddEditProductCommand>(item), cancellationToken);
                    if (validationResult.IsValid)
                    {
                        var exist = await _context.Products.AnyAsync(x => x.Name == item.Name, cancellationToken);
                        if (!exist)
                        {
                            await _context.Products.AddAsync(item, cancellationToken);
                        }
                    }
                    else
                    {
                        errorsOccurred = true;
                        errors.AddRange(validationResult.Errors.Select(e => $"{(!string.IsNullOrWhiteSpace(item.Name) ? $"{item.Name} - " : string.Empty)}{e.ErrorMessage}"));
                    }
                }

                if (errorsOccurred)
                {
                    return await Result.FailureAsync(errors);
                }

                await _context.SaveChangesAsync(cancellationToken);
                return await Result.SuccessAsync();
            }
            else
            {
                return await Result.FailureAsync(result.Errors);
            }
        }

        public async Task<byte[]> Handle(CreateProductsTemplateCommand request, CancellationToken cancellationToken)
        {
            var fields = new string[] {
                _localizer["Name"],
                _localizer["Description"],
                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer["Products"]);
            return result;
        }
    }
}
