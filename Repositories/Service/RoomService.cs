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
    public class RoomService : IRoomService
    {
        public void AddRoom(Room a) => RoomDAO.AddRoom(a);

        public void DeleteRoom(Room a) => RoomDAO.DeleteRoom(a);

        public List<Room> getRoombyType(string Type) => RoomDAO.getRoombyType(Type);


        public Room GetRoomById(string id) => RoomDAO.GetRoomById(id);


        public List<Room> GetRoomByST(int st) => RoomDAO.GetRoomByST(st);


        public List<Room> GetRooms() => RoomDAO.getAll();

        public Room SearchRoomByNumberRoom(string nRoom) => RoomDAO.SearchRoomByNumberRoom(nRoom);

        public void UpdateRoom(Room a) => RoomDAO.UpdateRoom(a);

    }
}
