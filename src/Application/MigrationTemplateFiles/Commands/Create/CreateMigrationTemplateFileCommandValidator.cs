// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.Commands.Create;

    public class CreateMigrationTemplateFileCommandValidator : AbstractValidator<CreateMigrationTemplateFileCommand>
    {
        public CreateMigrationTemplateFileCommandValidator()
        {
        RuleFor(v => v.Name)
            .MaximumLength(256)
            .NotEmpty();
        //RuleFor(v => v.ObjectField)
        //      .MaximumLength(256)
        //      .NotEmpty();
        RuleFor(v => v.FilePath)
              .MaximumLength(512)
              .NotEmpty();
    }
    }

