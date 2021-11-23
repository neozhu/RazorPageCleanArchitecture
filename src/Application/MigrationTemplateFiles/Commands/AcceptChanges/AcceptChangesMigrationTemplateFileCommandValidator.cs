// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.Commands.AcceptChanges;

    public class AcceptChangesMigrationTemplateFileCommandValidator : AbstractValidator<AcceptChangesMigrationTemplateFilesCommand>
    {
        public AcceptChangesMigrationTemplateFileCommandValidator()
        {
            
             RuleFor(v => v.Items)
                  .NotNull()
                  .NotEmpty();


    }
    }

