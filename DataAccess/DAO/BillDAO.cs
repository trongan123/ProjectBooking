using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BillDAO
    {
       public static List<Bill> getAll()
         {
            List<Bill> list = new List<Bill>();
            using (var context = new ASMBOOKINGContext())
            {
                list = context.Bills.ToList();
                foreach (var bill in list)
                {
                    bill.IdaccNavigation = AccountDAO.FindAccountById(bill.Idacc);
                }
            }
            return list;
        }
        public static Bill GetBillById(string id)
        {
            Bill a = new Bill();
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a = context.Bills.SingleOrDefault(x => x.Idbill.Equals(id));
                    if (a != null)
                    {
                        a.IdaccNavigation = AccountDAO.FindAccountById(a.Idacc);
                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return a;
        }
        public static String GetIDCuoi()
        {
            List<Bill> lists;

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    lists = context.Bills.Select((Bill i) => i).ToList();
                    if (lists.Count <= 0)
                    {
                        return "B0001";
                    }
                    string iDCuoi = lists.Last().Idbill;
                    return $"B{int.Parse(iDCuoi.Substring(1)) + 1:000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
      

        public static List<Bill> GetBillByAccount(string idAcc)
        {
            List<Bill> list = new List<Bill>();
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    list = context.Bills.Where(x => x.Idacc.Equals(idAcc)).ToList();
                    foreach (var bill in list)
                    {
                        bill.IdaccNavigation = AccountDAO.FindAccountById(bill.Idacc);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }
        public static void AddBill(Bill a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a.Idbill = GetIDCuoi();
                    context.Bills.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateBill(Bill a)
        {

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    context.Entry<Bill>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteBill(Bill a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    var p1 = context.Bills.SingleOrDefault(
                        x => x.Idbill == a.Idbill);
                    if (p1 != null)
                    {
                        context.Bills.Remove(p1);
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
