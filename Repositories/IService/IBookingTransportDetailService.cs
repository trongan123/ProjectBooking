using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IService
{
    public interface IBookingTransportDetailService
    {
        List<BookingTransportDetail> GetBookingTransportDetails(string idBill);
        void AddBookingTransportDetail(BookingTransportDetail a);
        BookingTransportDetail GetBookingTransportDetailById(string id);
        void DeleteBookingTransportDetail(BookingTransportDetail a);
        void UpdateBookingTransportDetail(BookingTransportDetail a);
    }
}
