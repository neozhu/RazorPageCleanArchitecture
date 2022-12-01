// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.Invoices.PaddleOCR
{
    public interface IInvoicesOcrJob
    {
        Task Recognition(int id, CancellationToken cancellationToken);
    }
}
