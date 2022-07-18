using DataAccess.DAO;
using Repositories.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Service
{
    public class ServiceService : IServiceService
    {
        public void AddService(DataAccess.Models.Service a) => ServiceDAO.AddService(a);
        public void DeleteService(DataAccess.Models.Service a) => ServiceDAO.DeleteService(a);
        public DataAccess.Models.Service GetServiceById(string id) => ServiceDAO.GetServiceById(id);
        public List<DataAccess.Models.Service> GetServices() => ServiceDAO.getAll();
        public void UpdateService(DataAccess.Models.Service a) => ServiceDAO.UpdateService(a);
    }
}
