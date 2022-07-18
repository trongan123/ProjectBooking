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
    public class CommentService : ICommentService
    {
        public void AddComment(Comment a) => CommentDAO.AddComment(a);
     

        public void DeleteComment(Comment a) => CommentDAO.DeleteComment(a);
        
        public Comment GetCommentById(string id) => CommentDAO.GetCommentById(id);


        public List<Comment> GetComments() => CommentDAO.getAll();
       
        public List<Comment> GetCommentsByAccount(string idAcc) => CommentDAO.GetCommentsByAccount(idAcc);
      

        public void UpdateComment(Comment a) => CommentDAO.UpdateComment(a);
       
    }
}
