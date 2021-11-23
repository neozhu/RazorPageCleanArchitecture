namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.Commands.Upload;

public class UploadMigrationTemplateFilesCommandValidator : AbstractValidator<UploadMigrationTemplateFilesCommand>
{
    public UploadMigrationTemplateFilesCommandValidator()
    {

        RuleFor(v => v.Data)
              .NotNull()
              .NotEmpty();


    }
}
