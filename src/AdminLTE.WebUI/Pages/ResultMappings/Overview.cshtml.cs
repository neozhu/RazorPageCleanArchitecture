using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Razor.Application.ResultMappings.Queries.Pagination;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.FieldMappingValues.Queries.Pagination;
using CleanArchitecture.Razor.Application.ResultMappings.Commands.AddEdit;
using Microsoft.AspNetCore.Mvc.Rendering;
using CleanArchitecture.Razor.Application.MigrationObjects.Queries.GetAll;
using CleanArchitecture.Razor.Application.ObjectFields.Queries.GetAll;
using CleanArchitecture.Razor.Application.ResultMappings.Commands.Parse;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Enums;
using CleanArchitecture.Razor.Application.ResultMappings.Commands.Delete;
using CleanArchitecture.Razor.Application.ResultMappings.Commands.ChangeStatus;
using CleanArchitecture.Razor.Application.ResultMappings.Queries.GetAll;
using CleanArchitecture.Razor.Application.ResultMappings.DTOs;

namespace AdminLTE.WebUI.Pages.ResultMappings
{
    [Authorize(Policy = "ProjectUsers")]
    public class OverviewModel : PageModel
    {

        [BindProperty]
        public AddEditResultMappingCommand Input { get; set; }

        [BindProperty]
        public IFormFile ResultMappingFile { get; set; }
        private readonly ICurrentUserService _currentUserService;
        private readonly ISender _mediator;
        private readonly IStringLocalizer<OverviewModel> _localizer;
        public SelectList MigrationObjects { get; set; }
        public List<StatusSummarizingDto> Summarizing { get; set; }

        public OverviewModel(
            ICurrentUserService currentUserService,
             ISender mediator,
            IStringLocalizer<OverviewModel> localizer
            )
        {
            _currentUserService = currentUserService;
            _mediator = mediator;
            _localizer = localizer;
        }
        public async Task OnGetAsync()
        {
            var request = new GetAllMigrationObjectsQuery();
            var objectlist = await _mediator.Send(request);
            MigrationObjects = new SelectList(objectlist, "Description", "Description");

            var summarizingrequest = new SummarizingByStatusQuery();
            var summarizing = await _mediator.Send(summarizingrequest);
            Summarizing = summarizing.ToList();

        }
        public async Task<IActionResult> OnPostDeleteCheckedAsync([FromBody] DeleteCheckedResultMappingsCommand command)
        {
            
            var result = await _mediator.Send(command);
            return new JsonResult("");
        }
        public async Task<IActionResult> OnPostAsync()
        {

            if (ResultMappingFile != null)
            {
                Input.UploadRequest = new UploadRequest();
                Input.UploadRequest.FileName = ResultMappingFile.FileName;
                Input.UploadRequest.UploadType = UploadType.ResultMappingFile;
                Input.UploadRequest.Folder = Input.RelevantMock;
                var stream = new MemoryStream();
                ResultMappingFile.CopyTo(stream);
                Input.UploadRequest.Data = stream.ToArray();
            }
            var result = await _mediator.Send(Input);
            return new JsonResult(result);

        }
        public async Task<IActionResult> OnGetDataAsync([FromQuery] ResultMappingsWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }

        public async Task<IActionResult> OnGetSummarizingVerifiedAsync([FromQuery]SummarizingVerifiedByIdQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }

        public async Task<IActionResult> OnPostVaildateResultMappingFile()
        {
            var stream = new MemoryStream();
            await ResultMappingFile.CopyToAsync(stream);
            var command = new ParseResultMappingFileCommand()
            {
                FileName = ResultMappingFile.FileName,
                Data = stream.ToArray()
            };
            var result = await _mediator.Send(command);
            return new JsonResult(result);



        }
        public async Task<IActionResult> OnGetResultMappingDataAsync([FromQuery] ResultMappingDataWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnGetFinished([FromQuery] FinishedResultMappingStatusCommand command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }

        public async Task<IActionResult> OnPostVerify([FromBody]VerifyResultMappingStatusCommand command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnPostScoped([FromBody]ScopedResultMappingStatusCommand command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
    }
}
