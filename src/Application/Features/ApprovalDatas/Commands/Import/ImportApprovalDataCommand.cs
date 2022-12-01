using CleanArchitecture.Razor.Application.Features.ApprovalDatas.DTOs;

namespace CleanArchitecture.Razor.Application.Features.ApprovalDatas.Commands.Import
{
    public class ImportApprovalDataCommand: ApprovalDataDto,IRequest<Result>
    {
      
    }

    public class ImportApprovalDataCommandHandler : IRequestHandler<ImportApprovalDataCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportApprovalDataCommandHandler> _localizer;
        public ImportApprovalDataCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<ImportApprovalDataCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public  Task<Result> Handle(ImportApprovalDataCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing ImportApprovalDataCommandHandler method 
           throw new System.NotImplementedException();
        }
    }
}
