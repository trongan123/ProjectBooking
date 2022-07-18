using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class RoomTypeDAO
    {
        public static List<RoomType> getAll()
        {

            List<RoomType> list = new List<RoomType>();
            using (var context = new ASMBOOKINGContext())
            {
                list = context.RoomTypes.ToList();
            }

            return list;
        }



        public static RoomType GetRoomTypeById(string id)
        {
            RoomType a = new RoomType();
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a = context.RoomTypes.SingleOrDefault(x => x.IdroomType.Equals(id));

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
            List<RoomType> list;

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    list = context.RoomTypes.Select((RoomType i) => i).ToList();
                    if (list.Count <= 0)
                    {
                        return "RT001";
                    }
                    string iDCuoi = list.Last().IdroomType;
                    return $"RT{int.Parse(iDCuoi.Substring(2)) + 1:00#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }


        public static void AddRoomType(RoomType a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a.IdroomType = GetIDCuoi();
                    context.RoomTypes.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateRoomType(RoomType a)
        {

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    context.Entry<RoomType>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteRoomType(RoomType a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    var p1 = context.RoomTypes.SingleOrDefault(
                        x => x.IdroomType == a.IdroomType);
                    if (p1 != null)
                    {
                        context.RoomTypes.Remove(p1);
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
