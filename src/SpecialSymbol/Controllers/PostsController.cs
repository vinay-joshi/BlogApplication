using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SpecialSymbol.Models;
using Microsoft.AspNet.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SpecialSymbol.Controllers
{
    public class PostsController : Controller
    {
        private BlogDataContext _blogDataContext;

        public PostsController(BlogDataContext blogDataContext)
        {
            _blogDataContext = blogDataContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            if (!ModelState.IsValid)
                return View(post);

            post.PostedDate = DateTime.Now;
            post.Author = User.Identity.Name;

            _blogDataContext.Posts.Add(post);
            await _blogDataContext.SaveChangesAsync();

            return RedirectToAction("Post", new { post.PostedDate.Year,post.PostedDate.Month,post.Key });
        }
        public ActionResult Post(long Id)
        {           
            var post = _blogDataContext.Posts.SingleOrDefault(x => x.Id == Id);
            return View(post);
        }

        [Route("posts/{year:int}/{month:int}/{key}")]
        public ActionResult Post(int year, int month,string key)
        {
            var post = _blogDataContext.Posts.SingleOrDefault(x => x.PostedDate.Year == year 
             && x.PostedDate.Month == month
             && x.Key == key.ToLower());
            return View(post);
        }
    }
}
