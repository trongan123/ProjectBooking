using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IService
{
    public interface IRoomTypeSevice
    {
        List<RoomType> GetRoomTypes();
        void AddRoomType(RoomType a);
        RoomType GetRoomTypeById(string id);
        void DeleteRoomType(RoomType a);
        void UpdateRoomType(RoomType a);
    }
}
