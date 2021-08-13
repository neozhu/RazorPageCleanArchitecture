// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;

namespace CleanArchitecture.Razor.Application.Documents.Commands.AddEdit
{
    public class AddEditDocumentCommandValidator : AbstractValidator<AddEditDocumentCommand>
    {
        public AddEditDocumentCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(256)
                .NotEmpty();
            RuleFor(v => v.DocumentTypeId)
                .GreaterThan(0);
            //RuleFor(v => v.UploadRequest)
            //    .NotNull();


        }
    }
}
