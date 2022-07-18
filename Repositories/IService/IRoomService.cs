using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IService
{
    public interface IRoomService
    {
        List<Room> GetRooms();
        List<Room> getRoombyType(string Type);
        Room GetRoomById(string id);
        Room SearchRoomByNumberRoom(String nRoom);
        List<Room> GetRoomByST(int st);
        void AddRoom(Room a);
        void UpdateRoom(Room a);
        void DeleteRoom(Room a);
    }
}
