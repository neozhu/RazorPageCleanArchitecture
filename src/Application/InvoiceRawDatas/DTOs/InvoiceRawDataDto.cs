// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.InvoiceRawDatas.DTOs
{
    public class InvoiceRawDataDto:IMapFrom<InvoiceRawData>
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Text { get; set; }
        public decimal Confidence { get; set; }
        public string Text_Region { get; set; }
        public string Label { get; set; }
    }
}
