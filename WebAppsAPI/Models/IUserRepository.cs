using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppsAPI.Models
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByEmail(string email);
        bool TryGetUser(int id, out User user);
        Post GetByPostId(int userId, int postId);
        IEnumerable<Comments> GetAllCommentsFromPost(int userId, int postId);
        IEnumerable<User> GetAll();

        void Add(User user);
        void Delete(User user);
        void Update(User user);
        void SaveChanges();
    }
}
