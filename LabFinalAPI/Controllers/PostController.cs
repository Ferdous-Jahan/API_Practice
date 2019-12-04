using LabFinalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LabFinalAPI.Controllers
{
    public class PostController : ApiController
    {
        LabDBEntities context = new LabDBEntities();

        // GET: api/Post
        //[Route("")]
        public IHttpActionResult Get()
        {
            var list = context.Posts.ToList();
            List<Post> posts = new List<Post>();
            foreach (var item in list)
            {
                Post p = new Post();
                p.Id = item.Id;
                p.Post1 = item.Post1;

                posts.Add(p);
            }
            return Ok(posts);
        }

        // GET: api/Post/5
        [Route("api/Post/{id}", Name = "GetPostById")]
        public IHttpActionResult Get(int id)
        {
            var item = context.Posts.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                Post p = new Post();
                p.Id = item.Id;
                p.Post1 = item.Post1;
                return Ok(p);
            }
        }

        // POST: api/Post
        //[Route("")]
        public IHttpActionResult Post(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
            return Created(Url.Link("GetPostById", new { id = post.Id }), post);
        }

        // PUT: api/Post/5
        [Route("api/Post/{id}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]Post post)
        {
            var item = context.Posts.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                item.Id = id;
                item.Post1 = post.Post1;

                Post p = new Post();
                p.Id = item.Id;
                p.Post1 = item.Post1;
                context.SaveChanges();
                return Ok(p);
            }
        }

        // DELETE: api/Post/5
        [Route ("api/Post/{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var item = context.Posts.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                context.Posts.Remove(item);
                context.SaveChanges();
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        [Route("api/Post/{id}/comments")]
        public IHttpActionResult GetCommentsByPost([FromUri]int id)
        {
            var list = context.Comments.Where(p => p.PostId == id);
            if (list.FirstOrDefault() == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                List<Comment> comm = new List<Comment>();
                foreach (var item in list)
                {
                    Comment c = new Comment();
                    c.Id = item.Id;
                    c.PostId = item.PostId;
                    c.Comment1 = item.Comment1;
                    comm.Add(c);
                }
                return Ok(comm);
            }
        }
    }
}
