// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Enums;

namespace CleanArchitecture.Razor.Application.Documents.DTOs
{
    public partial class DocumentDto : IMapFrom<Document>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Document, Document>().ReverseMap();

        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; } = false;
        public string URL { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
    }
}
