using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BookingSeviceDetailDAO
    {
        public static List<BookingSeviceDetail> getAll(string id)
        {

            List<BookingSeviceDetail> list = new List<BookingSeviceDetail>();
            using (var context = new ASMBOOKINGContext())
            {
                list = context.BookingSeviceDetails.Where(x => x.Idbill.Equals(id)).ToList();
                foreach(BookingSeviceDetail detail in list)
                {
                    detail.IdbillNavigation = BillDAO.GetBillById(detail.Idbill);
                    detail.IdseviceNavigation = ServiceDAO.GetServiceById(detail.Idsevice);
                }
            }

            return list;
        }



        public static BookingSeviceDetail GetBookingSeviceDetailById(string id)
        {
            BookingSeviceDetail a = new BookingSeviceDetail();
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a = context.BookingSeviceDetails.SingleOrDefault(x => x.IdbookingSeviceDetail.Equals(id));
                    if (a != null)
                    {
                        a.IdbillNavigation = BillDAO.GetBillById(a.Idbill);
                        a.IdseviceNavigation = ServiceDAO.GetServiceById(a.Idsevice);
                    }

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
            List<BookingSeviceDetail> list;

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    list = context.BookingSeviceDetails.Select((BookingSeviceDetail i) => i).ToList();
                    if (list.Count <= 0)
                    {
                        return "BS001";
                    }
                    string iDCuoi = list.Last().IdbookingSeviceDetail;
                    return $"BS{int.Parse(iDCuoi.Substring(2)) + 1:00#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }


        public static void AddBookingSeviceDetail(BookingSeviceDetail a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a.IdbookingSeviceDetail = GetIDCuoi();
                    context.BookingSeviceDetails.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateBookingSeviceDetail(BookingSeviceDetail a)
        {

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    context.Entry<BookingSeviceDetail>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteBookingSeviceDetail(BookingSeviceDetail a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    var p1 = context.BookingSeviceDetails.SingleOrDefault(
                        x => x.IdbookingSeviceDetail == a.IdbookingSeviceDetail);
                    if (p1 != null)
                    {
                        context.BookingSeviceDetails.Remove(p1);
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
