// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.FieldMappingValues.Commands.Import;

    public class ImportFieldMappingValuesCommandValidator : AbstractValidator<ImportFieldMappingValuesCommand>
    {
        public ImportFieldMappingValuesCommandValidator()
        {
           
           RuleFor(v => v.Data)
                 .NotNull()
                 .NotEmpty();
           
        }
    }

