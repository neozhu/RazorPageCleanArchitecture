// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationProjects.Commands.Delete;

    public class DeleteMigrationProjectCommandValidator : AbstractValidator<DeleteMigrationProjectCommand>
    {
        public DeleteMigrationProjectCommandValidator()
        {

         RuleFor(v => v.Id).NotNull().GreaterThan(0);

        }
    }
    public class DeleteCheckedMigrationProjectsCommandValidator : AbstractValidator<DeleteCheckedMigrationProjectsCommand>
    {
        public DeleteCheckedMigrationProjectsCommandValidator()
        {
       
        RuleFor(v => v.Id).NotNull().NotEmpty();

        }
    }

