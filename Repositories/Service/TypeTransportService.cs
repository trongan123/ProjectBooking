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
    public class TypeTransportService : ITypeTransportService
    {
        public void AddTypeTransport(TypeTransport a) => TypeTransportDAO.AddTypeTransport(a);

        public void DeleteTypeTransport(TypeTransport a)=> TypeTransportDAO.DeleteTypeTransport(a);

        public TypeTransport GetTypeTransportById(string id)=> TypeTransportDAO.GetTypeTransportById(id);
        public List<TypeTransport> GetTypeTransports() => TypeTransportDAO.getAll();
        public void UpdateTypeTransport(TypeTransport a) => TypeTransportDAO.UpdateTypeTransport(a);
    }
}
