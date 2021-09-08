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

namespace CleanArchitecture.Razor.Application.Products.Commands.Create
{
    public class CreateProductCommand: ProductDto,IRequest<Result>, IMapFrom<Product>
    {
      
    }
    

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateProductCommand> _localizer;
        public CreateProductCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateProductCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //TODO:Implementing CreateProductCommandHandler method 
            var item = _mapper.Map<Product>(request);
            _context.Products.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return  Result.Success();
        }
    }
}
