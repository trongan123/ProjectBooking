using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class RoomDAO
    {
        public static List<Room> getAll()
        {

            List<Room> list = new List<Room>();
            using (var _context = new ASMBOOKINGContext())
            {
                list = _context.Rooms.Where(r=> r.Stroom !=0).ToList();
                foreach(Room room in list)
                {
                    room.IdroomTypeNavigation = RoomTypeDAO.GetRoomTypeById(room.IdroomType);
                }
            }

            return list;
        }
        public static List<Room> getRoombyType(string Type)
        {

            List<Room> list = new List<Room>();
            using (var _context = new ASMBOOKINGContext())
            {
                list = _context.Rooms.Where(r => r.Stroom == 1 && r.IdroomType.Equals(Type)).ToList();
                foreach (Room room in list)
                {
                    room.IdroomTypeNavigation = RoomTypeDAO.GetRoomTypeById(room.IdroomType);
                }
            }

            return list;
        }

        public static Room GetRoomById(string id)
        {
            Room a = new Room();
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a = context.Rooms.SingleOrDefault(x => x.Idroom.Equals(id));
                    if(a != null)
                    {
                        a.IdroomTypeNavigation = RoomTypeDAO.GetRoomTypeById(a.IdroomType);
                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return a;
        }
        public static string GetIDCuoi()
        {
            List<Room> lists;

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    lists = context.Rooms.Select((Room i) => i).ToList();
                    if (lists.Count <= 0)
                    {
                        return "R0001";
                    }
                    string iDCuoi = lists.Last().Idroom;
                    return $"R{int.Parse(iDCuoi.Substring(1)) + 1:000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public static Room SearchRoomByNumberRoom(String nRoom)
        {
            Room a = new Room();
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a = context.Rooms.SingleOrDefault(x => x.NumberRoom.Equals(nRoom));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return a;
        }

        public static List<Room> GetRoomByST(int st)
        {
            List<Room> list = new List<Room>();
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    list = context.Rooms.Where(x => x.Stroom == st).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }
        public static void AddRoom(Room a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a.Idroom = GetIDCuoi();
                    context.Rooms.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateRoom(Room a)
        {

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    context.Entry<Room>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteRoom(Room a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    var p1 = context.Rooms.SingleOrDefault(
                        x => x.Idroom == a.Idroom);
                    if (p1 != null)
                    {
                        p1.Stroom=0;
                        context.Entry<Room>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
