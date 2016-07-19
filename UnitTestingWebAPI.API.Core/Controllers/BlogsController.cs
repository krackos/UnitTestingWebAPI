using System;
using System.Collections.Generic;
using System.Web.Http;
using UnitTestingWebAPI.Services;
using UnitTestingWebAPI.Domain;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace UnitTestingWebAPI.API.Core.Controllers
{
    public class BlogsController : ApiController
    {
        private IBlogService _blogService;
        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        // GET: api/Blogs
        public IEnumerable<Blog> GetBlogs()
        {
            return _blogService.GetBlogs();
        }
        // GET: api/Blogs/5
        [ResponseType(typeof(Blog))]
        public IHttpActionResult GetBlog(int id)
        {
            Blog blog = _blogService.GetBlog(id);
            if (blog == null)
                return NotFound();

            return Ok(blog);
        }
        // PUT: api/Blogs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBlog(int id, Blog blog)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != blog.ID)
                return BadRequest();

            _blogService.UpdateBlog(blog);

            try {
                _blogService.SaveBlog();
            } catch (DbUpdateConcurrencyException) {
                if (!BlogExists(id))
                    return NotFound();
                else
                    throw;
            }
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
        // POST: api/Blogs
        [ResponseType(typeof(Blog))]
        public IHttpActionResult PostBlog(Blog blog)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _blogService.CreateBlog(blog);

            return CreatedAtRoute("DefaultApi", new { id = blog.ID }, blog);
        }
        // DELETE: api/Blogs/5
        [ResponseType(typeof(Blog))]
        public IHttpActionResult DeleteBlog(int id)
        {
            Blog blog = _blogService.GetBlog(id);
            if (blog == null)
                return NotFound();

            _blogService.DeleteBlog(blog);

            return Ok(blog);
        }
        private bool BlogExists(int id)
        {
            return _blogService.GetBlog(id) != null;
        }

    }
}
