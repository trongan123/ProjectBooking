using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IService
{
    public interface ITransportService
    {
        List<Transport> GetTransports();
        Transport GetTransportById(string id);
        List<Transport> getTransportbyType(string Type);
        void AddTransport(Transport a);
        void UpdateTransport(Transport a);
        void DeleteTransport(Transport a);
    }
}
