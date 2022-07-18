using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class TypeTransportDAO
    {
        public static List<TypeTransport> getAll()
        {

            List<TypeTransport> list = new List<TypeTransport>();
            using (var context = new ASMBOOKINGContext())
            {
                list = context.TypeTransports.ToList();
            }

            return list;
        }



        public static TypeTransport GetTypeTransportById(string id)
        {
            TypeTransport a = new TypeTransport();
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a = context.TypeTransports.SingleOrDefault(x => x.IdtypeTransport.Equals(id));

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
            List<TypeTransport> list;

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    list = context.TypeTransports.Select((TypeTransport i) => i).ToList();
                    if (list.Count <= 0)
                    {
                        return "TT001";
                    }
                    string iDCuoi = list.Last().IdtypeTransport;
                    return $"TT{int.Parse(iDCuoi.Substring(2)) + 1:00#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }


        public static void AddTypeTransport(TypeTransport a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a.IdtypeTransport = GetIDCuoi();
                    context.TypeTransports.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateTypeTransport(TypeTransport a)
        {

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    context.Entry<TypeTransport>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteTypeTransport(TypeTransport a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    var p1 = context.TypeTransports.SingleOrDefault(
                        x => x.IdtypeTransport == a.IdtypeTransport);
                    if (p1 != null)
                    {
                        context.TypeTransports.Remove(p1);
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
