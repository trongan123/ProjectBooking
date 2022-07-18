using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IService
{
    public interface IBookingRoomDetailService
    {
        List<BookingRoomDetail> GetBookingRoomDetails(string idBill);
        void AddBookingRoomDetail(BookingRoomDetail a);
        BookingRoomDetail GetBookingRoomDetailById(string id);
        void DeleteBookingRoomDetail(BookingRoomDetail a);
        void UpdateBookingRoomDetail(BookingRoomDetail a);
        
    }
}
