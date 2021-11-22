// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationObjects.Commands.AcceptChanges;

public class AcceptChangesMigrationObjectCommandValidator : AbstractValidator<AcceptChangesMigrationObjectsCommand>
{
    public AcceptChangesMigrationObjectCommandValidator()
    {
        
        RuleFor(v => v.Items)
             .NotNull()
             .NotEmpty();


    }
}

