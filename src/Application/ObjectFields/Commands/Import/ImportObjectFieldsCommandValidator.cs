// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ObjectFields.Commands.Import;

    public class ImportObjectFieldsCommandValidator : AbstractValidator<ImportObjectFieldsCommand>
    {
        public ImportObjectFieldsCommandValidator()
        {
        
        RuleFor(v => v.Data)
              .NotNull()
              .NotEmpty();

    }
    }

