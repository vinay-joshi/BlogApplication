using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SpecialSymbol.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SpecialSymbol.Api
{
    [Route("api/posts/{postId:long}/comments")]
    public class CommentsController : Controller
    {
        private BlogDataContext _blogDataContext;

        public CommentsController(BlogDataContext blogDataContext )
        {
            _blogDataContext = blogDataContext;
        }
        // GET: api/values
        [HttpGet]
        public IQueryable<Comment> Get(long postId)
        {
            return _blogDataContext.Comments
                .Where(x=>x.PostId == postId);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public  async Task<Comment> Post(long postId,[FromBody]Comment comment)
        {
            comment.PostId = postId;
            comment.Author = comment.Author;
            comment.PostedDate = DateTime.Now;

            _blogDataContext.Comments.Add(comment);
            await _blogDataContext.SaveChangesAsync();
            return comment;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
