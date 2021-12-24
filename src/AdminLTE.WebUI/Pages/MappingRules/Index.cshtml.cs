using CleanArchitecture.Razor.Application.MappingRules.Commands.AddEdit;
using CleanArchitecture.Razor.Application.MappingRules.Commands.Delete;
using CleanArchitecture.Razor.Application.MappingRules.Queries.Export;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.MappingRules.Queries.Pagination;
using CleanArchitecture.Razor.Application.MigrationObjects.Queries.GetAll;
using CleanArchitecture.Razor.Application.MappingRules.Commands.Import;
using CleanArchitecture.Razor.Domain.Enums;
using CleanArchitecture.Razor.Application.FieldMappingValues.Commands.Import;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.MappingRules.Commands.Parse;
using CleanArchitecture.Razor.Application.ObjectFields.Queries.GetAll;
using CleanArchitecture.Razor.Application.ObjectFields.DTOs;
using CleanArchitecture.Razor.Application.MappingRules.Commands.Download;
using CleanArchitecture.Razor.Application.FieldMappingValues.Queries.Pagination;
using CleanArchitecture.Razor.Application.MappingRules.Commands.ChangeStatus;
using CleanArchitecture.Razor.Application.ResultMappings.DTOs;
using CleanArchitecture.Razor.Application.MappingRules.Queries.GetAll;
using CleanArchitecture.Razor.Application.FieldMappingValues.Commands.Delete;

namespace AdminLTE.WebUI.Pages.MappingRules
{
    [Authorize(Policy = "ProjectUsers")]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public AddEditMappingRuleCommand Input { get; set; }
        [BindProperty]
        public IFormFile UploadedFile { get; set; }
        [BindProperty]
        public IFormFile TemplateFile { get; set; }

        [BindProperty]
        public IFormFile FieldMappingDataFile { get; set; }
        [BindProperty]
        public int MappingRuleId { get; set; }
        [BindProperty]
        public string MappingRuleName { get; set; }
        public SelectList MigrationObjects { get; set; }
        public SelectList MigrationProjects { get; set; }
        public SelectList DataElements { get; set; }

        private readonly ICurrentUserService _currentUserService;
        private readonly ISender _mediator;
        private readonly IStringLocalizer<IndexModel> _localizer;
        public  IEnumerable<ObjectFieldDto> FieldList { get; set; }
        public List<StatusSummarizingDto> Summarizing { get; set; }
        public IndexModel(
            ICurrentUserService currentUserService,
                ISender mediator,
            IStringLocalizer<IndexModel> localizer
            )
        {
            _currentUserService = currentUserService;
            _mediator = mediator;
            _localizer = localizer;
        }
        public async Task OnGetAsync()
        {
            //var role = _currentUserService.IsInRole("DSC IT SAP");
            //var displayname = this.User.GetDisplayName();
    

            var request = new GetAllMigrationObjectsQuery();
            var objectlist = await _mediator.Send(request);
            MigrationObjects = new SelectList(objectlist, "Description", "Description");
            var requestfield = new GetAllObjectFieldsQuery();
            FieldList = await _mediator.Send(requestfield);
            DataElements = new SelectList(FieldList, "Name", "Name");
            var summarizingrequest = new SummarizingByStatusQuery();
            var summarizing = await _mediator.Send(summarizingrequest);
            Summarizing = summarizing.ToList();
        }
        public async Task<IActionResult> OnGetDataAsync([FromQuery] MappingRulesWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnPostAsync()
        {

            if (TemplateFile != null)
            {
                Input.UploadRequest = new  UploadRequest();
                Input.UploadRequest.FileName = TemplateFile.FileName;
                Input.UploadRequest.UploadType = UploadType.TemplateFile;
                var stream = new MemoryStream();
                TemplateFile.CopyTo(stream);
                Input.UploadRequest.Data = stream.ToArray();
            }
            var result = await _mediator.Send(Input);
            return new JsonResult(result);

        }

        public async Task<IActionResult> OnPostDeleteCheckedAsync([FromBody] DeleteCheckedMappingRulesCommand command)
        {
          ;
            var result = await _mediator.Send(command);
            return new JsonResult("");
        }
        public async Task<IActionResult> OnGetDeleteAsync([FromQuery] int id)
        {
            var command = new DeleteMappingRuleCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult("");
        }
        public async Task<FileResult> OnPostExportAsync([FromBody] ExportMappingRulesQuery command)
        {
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["MappingRules"] + ".xlsx");
        }
        public async Task<FileResult> OnGetCreateTemplate()
        {
            var command = new CreateMappingRulesTemplateCommand();
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["MappingRules"] + ".xlsx");
        }
        public async Task<IActionResult> OnPostImportAsync()
        {
            var stream = new MemoryStream();
            await UploadedFile.CopyToAsync(stream);
            var command = new ImportMappingRulesCommand()
            {
                FileName = UploadedFile.FileName,
                Data = stream.ToArray()
            };
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<FileResult> OnGetCreateDataTemplate(int mappingruleid,string mappingrulename)
        {
            var command = new CreateFieldMappingDataTemplateCommand();
            command.MappingRuleId = mappingruleid;
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer[$"FieldMappingValues({mappingrulename})"] + ".xlsx");
        }
        public async Task<FileResult> OnGetDownloadData(int mappingruleid,string mappingrulename)
        {
            var command = new DownloadMappingValueFileCommand();
            command.MappingRuleId = mappingruleid;
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{mappingrulename}.xml");
        }
        public async Task<IActionResult> OnPostUploadFieldMappingData() {
            var stream = new MemoryStream();
            await FieldMappingDataFile.CopyToAsync(stream);
            var command = new ImportDataFieldMappingValuesCommand()
            {
                MappingRuleId = MappingRuleId,
                FileName = FieldMappingDataFile.FileName,
                Data = stream.ToArray()
            };
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }

        public async Task<IActionResult> OnPostVaildateTemplateFile() {
            var stream = new MemoryStream();
            await TemplateFile.CopyToAsync(stream);
            var command = new ParseTemplateFileCommand() {
                FileName = TemplateFile.FileName,
                Data = stream.ToArray()
             };
            var result = await _mediator.Send(command);
            return new JsonResult(result);

   

        }
        public async Task<IActionResult> OnGetFieldMappingValuesAsync([FromQuery] FieldMappingValuesByMappingIdWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }

        public async Task<IActionResult> OnPostFinished([FromBody]FinishedMappingRuleStatusCommand command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }

        public async Task<IActionResult> OnPostDeleteCheckedFieldValue([FromBody] DeleteCheckedFieldMappingValuesCommand command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }

    }
}
