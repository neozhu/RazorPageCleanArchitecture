using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Customers.DTOs;
using CleanArchitecture.Razor.Application.Documents.DTOs;
using CleanArchitecture.Razor.Application.DocumentTypes.DTOs;
using CleanArchitecture.Razor.Application.Features.ApprovalDatas.DTOs;
using CleanArchitecture.Razor.Application.Features.Products.DTOs;
using CleanArchitecture.Razor.Application.KeyValues.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Entities.Worflow;
using NUnit.Framework;
using System;
using System.Runtime.Serialization;

namespace CleanArchitecture.Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                //cfg.Advanced.AllowAdditiveTypeMapCreation = true;
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }
        
        [Test]
        [TestCase(typeof(ApprovalData), typeof(ApprovalDataDto))]
        [TestCase(typeof(DocumentType), typeof(DocumentTypeDto))]
        [TestCase(typeof(Document), typeof(DocumentDto))]
        [TestCase(typeof(Customer), typeof(CustomerDto))]
        [TestCase(typeof(KeyValue), typeof(KeyValueDto))]
        [TestCase(typeof(Product), typeof(ProductDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }

        private object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type);

            // Type without parameterless constructor
            return FormatterServices.GetUninitializedObject(type);
        }
    }
}
