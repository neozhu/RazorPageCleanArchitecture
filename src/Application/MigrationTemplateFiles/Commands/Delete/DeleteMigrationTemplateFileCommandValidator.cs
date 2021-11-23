// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.Commands.Delete;

    public class DeleteMigrationTemplateFileCommandValidator : AbstractValidator<DeleteMigrationTemplateFileCommand>
    {
        public DeleteMigrationTemplateFileCommandValidator()
        {
          
           RuleFor(v => v.Id).NotNull().GreaterThan(0);
          
        }
    }
    public class DeleteCheckedMigrationTemplateFilesCommandValidator : AbstractValidator<DeleteCheckedMigrationTemplateFilesCommand>
    {
        public DeleteCheckedMigrationTemplateFilesCommandValidator()
        {
           
             RuleFor(v => v.Id).NotNull().NotEmpty();
           
        }
    }

