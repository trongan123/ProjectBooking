using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IService
{
    public interface ITypeTransportService
    {
        List<TypeTransport> GetTypeTransports();
        void AddTypeTransport(TypeTransport a);
        TypeTransport GetTypeTransportById(string id);
        void DeleteTypeTransport(TypeTransport a);
        void UpdateTypeTransport(TypeTransport a);
    }
}
