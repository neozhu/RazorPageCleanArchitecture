// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.KeyValues.Commands.AddEdit;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Enums;

namespace CleanArchitecture.Razor.Application.KeyValues.DTOs
{
    public partial class KeyValueDto:IMapFrom<KeyValue>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<KeyValueDto, KeyValue>()
                .ForMember(x=>x.DomainEvents,y=>y.Ignore())
                .ForMember(x=>x.Created,y=>y.Ignore())
                .ForMember(x => x.CreatedBy, y => y.Ignore())
                .ForMember(x => x.LastModified, y => y.Ignore())
                .ForMember(x => x.LastModifiedBy, y => y.Ignore());
            profile.CreateMap<KeyValue,KeyValueDto>()
                .ForMember(x=>x.TrackingState,y=>y.MapFrom(z=> TrackingState.Unchanged));
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public TrackingState TrackingState { get; set; }
    }
}
