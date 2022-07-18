using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CommentDAO
    {
        public static List<Comment> getAll()
        {

            List<Comment> list = new List<Comment>();
            using (var context = new ASMBOOKINGContext())
            {
                list = context.Comments.ToList();
                foreach (var a in list)
                {
                    a.IdaccNavigation = AccountDAO.FindAccountById(a.Idacc);
                }
            }

            return list;
        }

        public static List<Comment> GetCommentsByAccount(string idAcc)
        {
            List<Comment> list = new List<Comment>();
            using (var context = new ASMBOOKINGContext())
            {
                list = context.Comments.Where(x => x.Idacc.Equals(idAcc)).ToList();
                foreach (var a in list)
                {
                    a.IdaccNavigation = AccountDAO.FindAccountById(a.Idacc);
                }
            }

            return list;
        }


        public static Comment GetCommentById(string id)
        {
            Comment a = new Comment();
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a = context.Comments.SingleOrDefault(x => x.Idcomment.Equals(id));
                    if (a != null)
                    {
                        a.IdaccNavigation = AccountDAO.FindAccountById(a.Idacc);
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
            List<Comment> list;

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    list = context.Comments.Select((Comment i) => i).ToList();
                    if (list.Count <= 0)
                    {
                        return "C0001";
                    }
                    string iDCuoi = list.Last().Idcomment;
                    return $"C{int.Parse(iDCuoi.Substring(1)) + 1:000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }


        public static void AddComment(Comment a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    a.Idcomment = GetIDCuoi();
                    context.Comments.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateComment(Comment a)
        {

            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    context.Entry<Comment>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteComment(Comment a)
        {
            try
            {
                using (var context = new ASMBOOKINGContext())
                {
                    var p1 = context.Comments.SingleOrDefault(
                        x => x.Idcomment == a.Idcomment);
                    if (p1 != null)
                    {
                        context.Comments.Remove(p1);
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
