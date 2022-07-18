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
    public class BillService : IBillService
    {
        public void AddBill(Bill a) => BillDAO.AddBill(a);

        public void DeleteBill(Bill a) => BillDAO.DeleteBill(a);

        public List<Bill> GetBillByAccount(string id) => BillDAO.GetBillByAccount(id);

        public Bill GetBillById(string id) => BillDAO.GetBillById(id);

        public List<Bill> GetBills() => BillDAO.getAll();
        public void UpdateBill(Bill a) => BillDAO.UpdateBill(a);

        public string getidcuoi() =>BillDAO.GetIDCuoi();
    }
}
