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
    public class RoomTypeSevice : IRoomTypeSevice
    {
        public void AddRoomType(RoomType a) => RoomTypeDAO.AddRoomType(a);  

        public void DeleteRoomType(RoomType a) => RoomTypeDAO.DeleteRoomType(a);
 

        public RoomType GetRoomTypeById(string id) => RoomTypeDAO.GetRoomTypeById(id);


        public List<RoomType> GetRoomTypes() => RoomTypeDAO.getAll();
 

        public void UpdateRoomType(RoomType a)=> RoomTypeDAO.UpdateRoomType(a); 
    
    }
}
