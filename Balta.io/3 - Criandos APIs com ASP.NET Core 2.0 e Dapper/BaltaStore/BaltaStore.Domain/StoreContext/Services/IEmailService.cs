namespace BaltaStore.Domain.StoreContext.Services
{
    public interface IEmailService
    {
        void Send(string to, string from, string suject, string body);
    }
}