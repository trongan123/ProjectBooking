using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IService
{
    public interface IBookingSeviceDetailService
    {
        List<BookingSeviceDetail> GetBookingSeviceDetails(string idBill);
        void AddBookingSeviceDetail(BookingSeviceDetail a);
        BookingSeviceDetail GetBookingSeviceDetailById(string id);
        void DeleteBookingSeviceDetail(BookingSeviceDetail a);
        void UpdateBookingSeviceDetail(BookingSeviceDetail a);
    }
}
