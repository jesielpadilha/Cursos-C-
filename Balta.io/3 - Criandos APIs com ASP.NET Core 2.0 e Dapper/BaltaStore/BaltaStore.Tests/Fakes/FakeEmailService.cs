using BaltaStore.Domain.StoreContext.Services;

namespace BaltaStore.Tests.Fakes
{
    public class FakeEmailService : IEmailService
    {
        public void Send(string to, string from, string suject, string body)
        {

        }
    }
}