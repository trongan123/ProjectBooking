using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AccountDAO
    {
        public static List<Account> getAll()
        {

            List<Account> list = new List<Account>();
            using (var context = new ASMBOOKINGContext())
            {
                list = context.Accounts.Where(a => a.St != 0).ToList();
            }

            return list;
        }



        public static Account FindAccountById(string id)
        {
            Account a = new Account();
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a = context.Accounts.SingleOrDefault(x => x.Idacc.Equals(id));

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return a;
        }
        public static string GetIDCuoi()
        {
            List<Account> accounts;

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    accounts = context.Accounts.Select((Account i) => i).ToList();
                    if (accounts.Count <= 0)
                    {
                        return "A0001";
                    }
                    string iDCuoi = accounts.Last().Idacc;
                    return $"A{int.Parse(iDCuoi.Substring(1)) + 1:000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static Account Login(string Emai, string password)
        {
            Account a = new Account();
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a = context.Accounts.SingleOrDefault(x => x.Mail.Equals(Emai) && x.Password.Equals(password));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return a;
        }
        public static List<Account> SearchAccountByPhone(String phone)
        {
            List<Account> list = new List<Account>();
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    list = context.Accounts.Where(x => x.Phone.Contains(phone)).ToList();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }
        public static void AddAccount(Account a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a.Idacc = GetIDCuoi();
                    context.Accounts.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateAccount(Account a)
        {

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    context.Entry<Account>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteAccount(Account a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    var p1 = context.Accounts.SingleOrDefault(
                        x => x.Idacc == a.Idacc);
                    if (p1 != null)
                    {
                        p1.St = 0;
                        context.Entry<Account>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
