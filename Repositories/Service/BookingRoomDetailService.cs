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
    public class BookingRoomDetailService : IBookingRoomDetailService
    {
        public void AddBookingRoomDetail(BookingRoomDetail a) => BookingRoomDetailDAO.AddBookingRoomDetail(a);

        public void DeleteBookingRoomDetail(BookingRoomDetail a) => BookingRoomDetailDAO.DeleteBookingRoomDetail(a);

        public BookingRoomDetail GetBookingRoomDetailById(string id) => BookingRoomDetailDAO.GetBookingRoomDetailById(id);

        public List<BookingRoomDetail> GetBookingRoomDetails(string idBill) => BookingRoomDetailDAO.getAll(idBill);

        public void UpdateBookingRoomDetail(BookingRoomDetail a) => BookingRoomDetailDAO.UpdateBookingRoomDetail(a);   
    }
}
