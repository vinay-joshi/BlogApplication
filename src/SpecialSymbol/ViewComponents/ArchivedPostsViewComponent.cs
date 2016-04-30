using Microsoft.AspNet.Mvc;
using SpecialSymbol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpecialSymbol.ViewComponents
{
    [ViewComponent]
    public class ArchivedPostsViewComponent : ViewComponent
    {
        private BlogDataContext _blogDataContext;

        public ArchivedPostsViewComponent(BlogDataContext blogDataContext)
        {
            _blogDataContext = blogDataContext;
        }
        public IViewComponentResult Invoke()
        {
            var archivedPosts = _blogDataContext.GetArchivedPosts().ToArray();
            return View(archivedPosts);
        }
    }
}
