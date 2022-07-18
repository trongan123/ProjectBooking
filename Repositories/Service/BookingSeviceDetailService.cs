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
    public class BookingSeviceDetailService : IBookingSeviceDetailService
    {
        public void AddBookingSeviceDetail(BookingSeviceDetail a) => BookingSeviceDetailDAO.AddBookingSeviceDetail(a);

        public void DeleteBookingSeviceDetail(BookingSeviceDetail a) => BookingSeviceDetailDAO.DeleteBookingSeviceDetail(a);


        public BookingSeviceDetail GetBookingSeviceDetailById(string id) => BookingSeviceDetailDAO.GetBookingSeviceDetailById(id);


        public List<BookingSeviceDetail> GetBookingSeviceDetails(string idBill) => BookingSeviceDetailDAO.getAll(idBill);


        public void UpdateBookingSeviceDetail(BookingSeviceDetail a) => BookingSeviceDetailDAO.UpdateBookingSeviceDetail(a);
      
    }
}
