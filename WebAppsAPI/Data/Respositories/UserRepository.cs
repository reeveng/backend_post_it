using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppsAPI.Models;

namespace WebAppsAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(UserContext dbContext)
        {
            _context = dbContext;
            _users = dbContext.Users;
        }
        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _users.Include(u => u.Posts).ThenInclude(p => p.Comments);
        }

        public User GetById(int id)
        {
            return _users.Include(u => u.Posts).ThenInclude(p => p.Comments).SingleOrDefault(u => u.Id == id);
        }
        public User GetByEmail(string email)
        {
            return _users.Include(u => u.Posts).ThenInclude(p => p.Comments).SingleOrDefault(u => u.Email == email);
        }
        public Post GetByPostId(int userId, int postId)
        {
            User user = GetById(userId);
            return user.Posts.SingleOrDefault(p => p.Id == postId);
        }
        public IEnumerable<Comments> GetAllCommentsFromPost(int userId, int postId)
        {
            Post post = GetByPostId(userId, postId);
            return post.Comments;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool TryGetUser(int id, out User user)
        {
            user = _context.Users.Include(u => u.Posts).FirstOrDefault(u => u.Id == id);
            return user != null;
        }

        public void Update(User user)
        {
            _context.Update(user);
        }


    }
}
