using CleanArchitecture.Razor.Application.Settings;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.Common.Interfaces
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}