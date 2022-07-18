using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BookingRoomDetailDAO
    {
        public static List<BookingRoomDetail> getAll(string id)
        {

            List<BookingRoomDetail> list = new List<BookingRoomDetail>();
            using (var context = new ASMBOOKINGContext())
            {
                list = context.BookingRoomDetails.Where(x=>x.Idbill.Equals(id)).ToList();
                
                foreach (var item in list)
                {
                    item.IdbillNavigation = BillDAO.GetBillById(item.Idbill);
                    item.IdroomNavigation = RoomDAO.GetRoomById(item.Idroom);
                }
            }
            return list;
        }



        public static BookingRoomDetail GetBookingRoomDetailById(string id)
        {
            BookingRoomDetail a = new BookingRoomDetail();
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a = context.BookingRoomDetails.SingleOrDefault(x => x.IdbookingRoomDetail.Equals(id));
                    if (a != null)
                    {
                        a.IdbillNavigation = BillDAO.GetBillById(a.Idbill);
                        a.IdroomNavigation = RoomDAO.GetRoomById(a.Idroom);
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
            List<BookingRoomDetail> list;

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    list = context.BookingRoomDetails.Select((BookingRoomDetail i) => i).ToList();
                    if (list.Count <= 0)
                    {
                        return "BR001";
                    }
                    string iDCuoi = list.Last().IdbookingRoomDetail;
                    return $"BR{int.Parse(iDCuoi.Substring(2)) + 1:00#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

       
        public static void AddBookingRoomDetail(BookingRoomDetail a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a.IdbookingRoomDetail = GetIDCuoi();
                    context.BookingRoomDetails.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateBookingRoomDetail(BookingRoomDetail a)
        {

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    context.Entry<BookingRoomDetail>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteBookingRoomDetail(BookingRoomDetail a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    var p1 = context.BookingRoomDetails.SingleOrDefault(
                        x => x.IdbookingRoomDetail == a.IdbookingRoomDetail);
                    if (p1 != null)
                    {
                        context.BookingRoomDetails.Remove(p1);
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
