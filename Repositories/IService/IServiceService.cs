using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Repositories.IService
{
    public interface IServiceService
    {
        List<DataAccess.Models.Service> GetServices();
        DataAccess.Models.Service GetServiceById(string id);
        void AddService(DataAccess.Models.Service a);
        void UpdateService(DataAccess.Models.Service a);
        void DeleteService(DataAccess.Models.Service a);
    }
}
