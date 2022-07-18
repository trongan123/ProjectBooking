using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IService
{
    public interface IAccountService
    {
        List<Account> GetAccounts();
        void AddAccount(Account a);
        Account GetAccountById(string id);
        void DeleteAccount(Account a);
        void UpdateAccount(Account a);
        List<Account> GetAccountsByPhone(String phone);
        Account Login(string email, string password);


    }
}
