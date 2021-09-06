using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.Products.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.Products.Commands.AddEdit
{
    public class AddEditProductCommand: ProductDto,IRequest<Result>, IMapFrom<Product>
    {
      
    }

    public class AddEditProductCommandHandler : IRequestHandler<AddEditProductCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditProductCommandHandler> _localizer;
        public AddEditProductCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditProductCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(AddEditProductCommand request, CancellationToken cancellationToken)
        {
            //TODO:Implementing AddEditProductCommandHandler method 
            if (request.Id > 0)
            {
                var item = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
                item = _mapper.Map(request, item);
            }
            else
            {
                var item = _mapper.Map<Product>(request);
                _context.Products.Add(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
