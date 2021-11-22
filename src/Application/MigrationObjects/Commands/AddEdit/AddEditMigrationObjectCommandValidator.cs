// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationObjects.Commands.AddEdit;

    public class AddEditMigrationObjectCommandValidator : AbstractValidator<AddEditMigrationObjectCommand>
    {
        public AddEditMigrationObjectCommandValidator()
        {
       
           RuleFor(v => v.Name)
                 .MaximumLength(256)
                 .NotEmpty();
 
        }
    }

