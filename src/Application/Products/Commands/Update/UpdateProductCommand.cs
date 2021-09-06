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

namespace CleanArchitecture.Razor.Application.Products.Commands.Update
{
    public class UpdateProductCommand: ProductDto,IRequest<Result>, IMapFrom<Product>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, UpdateProductCommand>().ReverseMap();
        }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateProductCommandHandler> _localizer;
        public UpdateProductCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateProductCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            //TODO:Implementing UpdateProductCommandHandler method 
            var item =await _context.Products.FindAsync( new object[] { request.Id }, cancellationToken);
            if (item != null)
            {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return Result.Success();
        }
    }
}
