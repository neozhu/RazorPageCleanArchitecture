namespace CleanArchitecture.Razor.Application.ObjectFields.Commands.Upload;

public class UploadFieldTemplateFilesCommandValidator : AbstractValidator<UploadFieldTemplateFilesCommand>
{
    public UploadFieldTemplateFilesCommandValidator()
    {

        RuleFor(v => v.Data)
              .NotNull()
              .NotEmpty();


    }
}
