
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using CleanArchitecture.Razor.Application.Documents.Commands.Delete;
using CleanArchitecture.Razor.Application.Common.Exceptions;
using CleanArchitecture.Razor.Application.Documents.Commands.AddEdit;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Application.DocumentTypes.Commands.AddEdit;
using CleanArchitecture.Razor.Application.DocumentTypes.Commands.Delete;

namespace CleanArchitecture.Application.IntegrationTests.Documents.Commands
{
    using static Testing;

    public class DeleteDocumentTypeTests : TestBase
    {
        [Test]
        public void ShouldRequireValidDocumentTypeId()
        {
            var command = new DeleteDocumentTypeCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteDocumentType()
        {
            var addcommand = new AddEditDocumentTypeCommand()
            {
                Name = "Word",
                Description = "For Test"
            };
           var result= await SendAsync(addcommand);
             
            await SendAsync(new DeleteDocumentTypeCommand
            {
                Id = result.Data
            });

            var item = await FindAsync<Document>(result.Data);

            item.Should().BeNull();
        }
         
    }
}
