using Tugas2FE.ViewModels;

namespace Tugas2FE.Interfaces
{
    public interface IAccount
    {
        Task<Account> Insert(Account obj);
        Task<SLogin> Authenticate(Account obj);

    }
}
