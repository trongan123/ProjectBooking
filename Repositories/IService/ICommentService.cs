using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IService
{
    public interface ICommentService
    {
        List<Comment> GetComments();
        void AddComment(Comment a);
        Comment GetCommentById(string id);
        void DeleteComment(Comment a);
        void UpdateComment(Comment a);
        List<Comment> GetCommentsByAccount(string idAcc);
    }
}
