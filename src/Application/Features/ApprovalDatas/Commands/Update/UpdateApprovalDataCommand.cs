using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.Features.ApprovalDatas.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.Features.ApprovalDatas.Commands.Update
{
    public class UpdateApprovalDataCommand: ApprovalDataDto,IRequest<Result>
    {
      
    }

    public class UpdateApprovalDataCommandHandler : IRequestHandler<UpdateApprovalDataCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateApprovalDataCommandHandler> _localizer;
        public UpdateApprovalDataCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateApprovalDataCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateApprovalDataCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing UpdateApprovalDataCommandHandler method 
           throw new System.NotImplementedException();
        }
    }
}
