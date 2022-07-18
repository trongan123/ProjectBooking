using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ServiceDAO
    {
        public static List<Service> getAll()
        {

            List<Service> list = new List<Service>();
            using (var context = new ASMBOOKINGContext())
            {
                list = context.Services.ToList();
            }

            return list;
        }



        public static Service GetServiceById(string id)
        {
            Service a = new Service();
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a = context.Services.SingleOrDefault(x => x.Idservice.Equals(id));

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
            List<Service> list;

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    list = context.Services.Select((Service i) => i).ToList();
                    if (list.Count <= 0)
                    {
                        return "S0001";
                    }
                    string iDCuoi = list.Last().Idservice;
                    return $"S{int.Parse(iDCuoi.Substring(1)) + 1:000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }


        public static void AddService(Service a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a.Idservice = GetIDCuoi();
                    context.Services.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateService(Service a)
        {

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    context.Entry<Service>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteService(Service a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    var p1 = context.Services.SingleOrDefault(
                        x => x.Idservice == a.Idservice);
                    if (p1 != null)
                    {
                        context.Services.Remove(p1);
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
