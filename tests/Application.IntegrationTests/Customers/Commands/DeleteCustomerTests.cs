
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using CleanArchitecture.Razor.Application.Customers.Commands.Delete;
using CleanArchitecture.Razor.Application.Common.Exceptions;
using CleanArchitecture.Razor.Application.Customers.Commands.AddEdit;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class DeleteCustomerTests : TestBase
    {
        [Test]
        public void ShouldRequireValidCustomerId()
        {
            var command = new DeleteCustomerCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteCustomer()
        {
            var result = await SendAsync(new AddEditCustomerCommand
            {
                Name = "Name",
                NameOfEnglish = "NameOfEnglish",
                GroupName = "GroupName",
                Region = "Region",
                Sales = "Sales",
                RegionSalesDirector = "RegionSalesDirector",
                PartnerType = "IC"
            });
            await SendAsync(new DeleteCustomerCommand
            {
                Id = result.Data
            });

            var item = await FindAsync<Customer>(result.Data);

            item.Should().BeNull();
        }
        [Test]
        public async Task ShouldDeleteCheckedCustomer()
        {
            var result1 = await SendAsync(new AddEditCustomerCommand
            {
                Name = "Name",
                NameOfEnglish = "NameOfEnglish",
                GroupName = "GroupName",
                Region = "Region",
                Sales = "Sales",
                RegionSalesDirector = "RegionSalesDirector",
                PartnerType = "IC"
            });
            var result2 = await SendAsync(new AddEditCustomerCommand
            {
                Name = "Name",
                NameOfEnglish = "NameOfEnglish",
                GroupName = "GroupName",
                Region = "Region",
                Sales = "Sales",
                RegionSalesDirector = "RegionSalesDirector",
                PartnerType = "IC"
            });
            await SendAsync(new DeleteCheckedCustomersCommand
            {
                Id = new int[] {result1.Data,result2.Data }
            });

            var item = await FindAsync<Customer>(result1.Data);
            item.Should().BeNull();
        }
    }
}
