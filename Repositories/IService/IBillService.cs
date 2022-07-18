using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IService
{
    public interface IBillService
    {
        List<Bill> GetBills();
        string getidcuoi();
       
        void AddBill(Bill a);
        Bill GetBillById(string id);
        void DeleteBill(Bill a);
        void UpdateBill(Bill a);
        List<Bill> GetBillByAccount(string id);
    }
}
