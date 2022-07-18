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
    public class BookingTransportDetailService : IBookingTransportDetailService
    {
        public void AddBookingTransportDetail(BookingTransportDetail a) => BookingTransportDetailDAO.AddBookingTransportDetail(a);

        public void DeleteBookingTransportDetail(BookingTransportDetail a) => BookingTransportDetailDAO.DeleteBookingTransportDetail(a);
       

        public BookingTransportDetail GetBookingTransportDetailById(string id) => BookingTransportDetailDAO.GetBookingTransportDetailById(id);


        public List<BookingTransportDetail> GetBookingTransportDetails(string idBill) => BookingTransportDetailDAO.getAll(idBill);


        public void UpdateBookingTransportDetail(BookingTransportDetail a) => BookingTransportDetailDAO.UpdateBookingTransportDetail(a);
       
    }
}
