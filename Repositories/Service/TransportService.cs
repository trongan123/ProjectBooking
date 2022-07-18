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
    public class TransportService : ITransportService
    {
        public void AddTransport(Transport a) => TransportDAO.AddTransport(a);

        public void DeleteTransport(Transport a) => TransportDAO.DeleteTransport(a);

        public Transport GetTransportById(string id) => TransportDAO.GetTransportById(id);

        public List<Transport> getTransportbyType(string Type) => TransportDAO.getTransportbyType(Type);
        public List<Transport> GetTransports() => TransportDAO.getAll();

        public void UpdateTransport(Transport a) => TransportDAO.UpdateTransport(a);
    }
}
