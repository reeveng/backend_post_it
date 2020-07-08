using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using WebAppsAPI.DTO;
using WebAppsAPI.Models;
using Microsoft.AspNetCore.Authorization;


namespace WebAppsAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository context)
        {
            _userRepository = context;
        }
        // GET: api/Users
        /// <summary>
        /// Get all users ordered by FirstName
        /// </summary>
        /// <returns>array of Users</returns>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll().OrderBy(u => u.FirstName);
        }



        /// <summary>
        /// Get a Post of an User
        /// </summary>
        /// <param name="id">id of the User</param>
        /// <param name="postId">id of the Post</param>
        [HttpGet("{id}/post/{postId}")]
        [AllowAnonymous]
        public ActionResult<Post> GetPost(int id, int postId)
        {
            if (!_userRepository.TryGetUser(id, out var user))
            {
                return NotFound();
            }
            Post post = user.GetPost(postId);
            if (post == null)
                return NotFound();
            return post;
        }


        /// <summary>
        /// Add a post to the actual user
        /// </summary>
        [HttpPost]
        public ActionResult<Post> AddPost(PostDTO post)
        {

            User user = _userRepository.GetByEmail(User.Identity.Name);
            var postToCreate = new Post(post.Title, post.Text);
            user.AddPost(postToCreate);
            _userRepository.SaveChanges();
            return CreatedAtAction(nameof(GetPost), new { id = user.Id, postId = postToCreate.Id }, postToCreate);
        }

        /// <summary>
        /// Adds a comment
        /// </summary>
        /// <param name="post">the selected post</param>
        /// <param name="comment">the comment to be added</param>
        [HttpPost("post/{title}/comments")]
        public ActionResult<Post> AddComment(string title, CommentsDTO comment)
        {
            User user = _userRepository.GetByEmail(User.Identity.Name);
            Post postToModify = user.GetPostByTitle(title);
            if (postToModify == null)
            {
                return NotFound();
            }

            var commentToAdd = new Comments(comment.Text);
            postToModify.AddComments(commentToAdd);
            _userRepository.SaveChanges();
            return CreatedAtAction(nameof(GetCommentFromPost), new { userId = user.Id, postId = postToModify.Id, commentId = commentToAdd.Id }, commentToAdd);

        }
        /// <summary>
        /// Retrieves all comments of a post
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <param name="postId">the id of the post</param>
        [HttpGet("{userId}/post/{postId}/comments")]
        [AllowAnonymous]
        public IEnumerable<Comments> GetCommentFromPost(int userId, int postId)
        {
            return _userRepository.GetAllCommentsFromPost(userId, postId);
        }
    }
}
