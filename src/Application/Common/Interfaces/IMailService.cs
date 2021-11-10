using CleanArchitecture.Razor.Application.Settings;

namespace CleanArchitecture.Razor.Application.Common.Interfaces
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}