﻿using FiorellaApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorellaApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly FiorelloDbContext _fiorelloDbContext;
        public BlogController(FiorelloDbContext fiorelloDbContext)
        {
            _fiorelloDbContext = fiorelloDbContext;
        }
        public IActionResult Index()
        {
            ViewBag.BlogCount = _fiorelloDbContext.Blogs.Count();
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var blog = _fiorelloDbContext.Blogs.AsNoTracking().FirstOrDefault(b => b.Id == id);
            if (blog==null)
            {
                return NotFound();
            }
            return View(blog);
        }
        public IActionResult LoadMore(int offset=3)
        {
            var datas = _fiorelloDbContext.Blogs.Skip(offset).Take(3).ToList();
            return PartialView("_BlogPartialView", datas);
        }
        public IActionResult SearchBlog(string text)
        {
            var datas = _fiorelloDbContext.Blogs
                .Where(b=>b.Title.ToLower().Contains(text.ToLower()))
                .OrderByDescending(b=>b.Id)
                .Take(3)
                .ToList();
            return PartialView("_SearchPartialView",datas);
        }
    }
}
