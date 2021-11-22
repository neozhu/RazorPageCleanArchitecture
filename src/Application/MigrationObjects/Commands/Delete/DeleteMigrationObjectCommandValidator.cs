// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationObjects.Commands.Delete;

    public class DeleteMigrationObjectCommandValidator : AbstractValidator<DeleteMigrationObjectCommand>
    {
        public DeleteMigrationObjectCommandValidator()
        {

           RuleFor(v => v.Id).NotNull().GreaterThan(0);
      
        }
    }
    public class DeleteCheckedMigrationObjectsCommandValidator : AbstractValidator<DeleteCheckedMigrationObjectsCommand>
    {
        public DeleteCheckedMigrationObjectsCommandValidator()
        {

            RuleFor(v => v.Id).NotNull().NotEmpty();
           
        }
    }

