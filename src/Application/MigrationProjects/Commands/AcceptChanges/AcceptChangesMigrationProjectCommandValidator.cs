// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationProjects.Commands.AcceptChanges;

public class AcceptChangesMigrationProjectCommandValidator : AbstractValidator<AcceptChangesMigrationProjectsCommand>
{
    public AcceptChangesMigrationProjectCommandValidator()
    {

        RuleFor(v => v.Items)
              .NotNull()
              .NotEmpty();
        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(x => x.Name).MaximumLength(256)
                                     .NotEmpty();
        });


    }
}

