using DataAccess.DAO;
using DataAccess.Models;
using Repositories.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Service
{
    public class AccountService : IAccountService
    {
        public void DeleteAccount(Account a) => AccountDAO.DeleteAccount(a);
        public Account GetAccountById(string id) => AccountDAO.FindAccountById(id);
        public List<Account> GetAccounts() => AccountDAO.getAll();
        public List<Account> GetAccountsByPhone(string phone) => AccountDAO.SearchAccountByPhone(phone);
        public void AddAccount(Account a) => AccountDAO.AddAccount(a);
        public void UpdateAccount(Account a) => AccountDAO.UpdateAccount(a);
        public Account Login(string email, string password)=> AccountDAO.Login(email, password);
    }
}
