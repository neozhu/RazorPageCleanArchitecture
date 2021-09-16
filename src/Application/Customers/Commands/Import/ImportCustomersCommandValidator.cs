// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;

namespace CleanArchitecture.Razor.Application.Customers.Commands.Import
{
    public class ImportCustomersCommandValidator : AbstractValidator<ImportCustomersCommand>
    {
        public ImportCustomersCommandValidator()
        {
            RuleFor(x => x.Data).NotNull().NotEmpty();
        }
    }
   
}
