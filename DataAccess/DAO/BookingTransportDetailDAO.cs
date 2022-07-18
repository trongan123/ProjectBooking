using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BookingTransportDetailDAO
    {
        public static List<BookingTransportDetail> getAll(string id)
        {

            List<BookingTransportDetail> list = new List<BookingTransportDetail>();
            using (var context = new ASMBOOKINGContext())
            {
                list = context.BookingTransportDetails.Where(x => x.Idbill.Equals(id)).ToList();
                foreach(BookingTransportDetail a in list)
                {
                    a.IdbillNavigation = BillDAO.GetBillById(a.Idbill);
                    a.IdtransportNavigation = TransportDAO.GetTransportById(a.Idtransport);
                }
            }

            return list;
        }



        public static BookingTransportDetail GetBookingTransportDetailById(string id)
        {
            BookingTransportDetail a = new BookingTransportDetail();
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a = context.BookingTransportDetails.SingleOrDefault(x => x.IdbookingTransportDetail.Equals(id));
                    if (a != null)
                    {
                        a.IdbillNavigation = BillDAO.GetBillById(a.Idbill);
                        a.IdtransportNavigation = TransportDAO.GetTransportById(a.Idtransport);
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
            List<BookingTransportDetail> list;

            try
            {

                using (var context = new ASMBOOKINGContext())
                {
                    list = context.BookingTransportDetails.Select((BookingTransportDetail i) => i).ToList();
                    if (list.Count <= 0)
                    {
                        return "BT001";
                    }
                    string iDCuoi = list.Last().IdbookingTransportDetail;
                    return $"BT{int.Parse(iDCuoi.Substring(2)) + 1:00#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static void AddBookingTransportDetail(BookingTransportDetail a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a.IdbookingTransportDetail = GetIDCuoi();
                    context.BookingTransportDetails.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateBookingTransportDetail(BookingTransportDetail a)
        {

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    context.Entry<BookingTransportDetail>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteBookingTransportDetail(BookingTransportDetail a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    var p1 = context.BookingTransportDetails.SingleOrDefault(
                        x => x.IdbookingTransportDetail == a.IdbookingTransportDetail);
                    if (p1 != null)
                    {
                        context.BookingTransportDetails.Remove(p1);
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
