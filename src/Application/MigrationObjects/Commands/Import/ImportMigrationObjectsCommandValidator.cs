// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationObjects.Commands.Import;

public class ImportMigrationObjectsCommandValidator : AbstractValidator<ImportMigrationObjectsCommand>
{
    public ImportMigrationObjectsCommandValidator()
    {

        RuleFor(v => v.Data)
              .NotNull()
              .NotEmpty();

    }
}

