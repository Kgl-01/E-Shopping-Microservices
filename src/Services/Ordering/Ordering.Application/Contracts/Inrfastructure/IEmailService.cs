using Ordering.Application.Models;

namespace Ordering.Application.Contracts.Inrfastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
