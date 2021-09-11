using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.InvoiceDetails.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.InvoiceDetails.Commands.Create
{
    public class CreateInvoiceDetailCommand: InvoiceDetailDto,IRequest<Result>, IMapFrom<InvoiceDetail>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<InvoiceDetail, CreateInvoiceDetailCommand>().ReverseMap();
        }
    }
    

    public class CreateInvoiceDetailCommandHandler : IRequestHandler<CreateInvoiceDetailCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateInvoiceDetailCommand> _localizer;
        public CreateInvoiceDetailCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateInvoiceDetailCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(CreateInvoiceDetailCommand request, CancellationToken cancellationToken)
        {

           var item = _mapper.Map<InvoiceDetail>(request);
            _context.InvoiceDetails.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return  Result.Success();
        }
    }
}
