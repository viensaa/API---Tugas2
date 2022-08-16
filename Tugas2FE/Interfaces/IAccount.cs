using Tugas2FE.ViewModels;

namespace Tugas2FE.Interfaces
{
    public interface IAccount
    {
        Task<Account> Register(Account obj);
        Task<SLogin> Authenticate(string username, string password);

    }
}
