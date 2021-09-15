// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;

namespace CleanArchitecture.Razor.Application.DocumentTypes.Commands.Import
{
    public class ImportDocumentTypesCommandValidator : AbstractValidator<ImportDocumentTypesCommand>
    {
        public ImportDocumentTypesCommandValidator()
        {
            RuleFor(x => x.Data).NotNull().NotEmpty();
        }
    }
   
}
