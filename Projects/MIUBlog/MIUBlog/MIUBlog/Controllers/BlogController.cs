using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MIUBlog.Business.Abstract;
using MIUBlog.Entities.Concrete;
using MIUBlog.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MIUBlog.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService _blogService;
        private ICategoryService _categoryService;
        private ICommentService _commentService;
        private UserManager<ApplicationUser> _userManager;
        public BlogController(IBlogService blogService, ICategoryService categoryService, ICommentService commentService, UserManager<ApplicationUser> userManager)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _commentService = commentService;
            _userManager = userManager;
        }



        public IActionResult Index(int? id, string q)
        {
            var query = _blogService.GetAll().Where(i => i.isApproved);
            if (id != null)
            {
                query = _blogService.GetAll().Where(i => i.CategoryId == id);
            }
            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(i => EF.Functions.Like(i.Title, "%" + q + "%") || i.Description.ToLower().Contains(q.ToLower()) || i.Body.ToLower().Contains(q.ToLower()));
            }
            return View(query.OrderByDescending(i => i.Date));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult List()
        {
            return View(_blogService.GetAll());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "CategoryId", "Name");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Blog entity)
        {
            TempData["action"] = "Create";


            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                entity.ApplicationUserId = user.Id;
                _blogService.Add(entity);
                return RedirectToAction("List");
            }
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "CategoryId", "Name");
            return View(entity);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "CategoryId", "Name");

            return View(_blogService.GetAll().Where(i => i.BlogId == id).FirstOrDefault());

        }

        public IActionResult Details(int id, Blog blg)
        {
            int blogId = id;
            if (id == 0)
            {
                blogId = blg.BlogId;
            }


            var blog = _blogService.Get(blogId);
            var comments = _commentService.GetAll()
                .Include(i => i.ApplicationUser)
                .Where(i => i.Blog.BlogId == blogId && i.isApproved == true);
            BlogModel model = new BlogModel()
            {
                Blog = blog,
                Comments = comments
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Blog entity, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    entity.Image = file.FileName;
                }


                _blogService.Update(entity);
                TempData["message"] = $"{entity.Title} güncellendi.";
                return RedirectToAction("List");
            }
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "CategoryId", "Name");
            return View(entity);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var result = _blogService.GetAll().Where(i => i.BlogId == id).FirstOrDefault();
            TempData["message"] = $"{result.Title } Silmek İstediğinizden Eminmisiniz";


            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmate(int BlogId)
        {
            try
            {
                var result = _blogService.GetAll().Where(i => i.BlogId == BlogId).FirstOrDefault();
                TempData["message"] = $"{result.Title} Silindi";
                if (ModelState.IsValid)
                {
                    _blogService.Delete(BlogId);

                }

                return RedirectToAction("List");
            }
            catch (DbUpdateException e)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = "Önce alt kayıtları silmelisiniz.";
                return View("Error", errorViewModel);
            }
            catch (Exception e)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = e.Message;
                return View("Error", errorViewModel);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddOrUpdate(int? id)
        {

            if (id == 0 || id == null)
            {

                ViewData["title"] = "Create";
                TempData["action"] = "Create";
                return View("Create");
            }
            else
            {
                TempData["action"] = "Update";
                ViewData["title"] = "Update";
                return View("Create", _blogService.GetAll().FirstOrDefault(i => i.BlogId == id));

            }


        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task< IActionResult> AddOrUpdate(Blog entity, IFormFile file)
        {
            if (entity.BlogId == 0)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    entity.Image = file.FileName;
                }
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                entity.ApplicationUserId = user.Id;

                _blogService.Add(entity);
                ViewData["title"] = "Create";
                TempData["action"] = "Create";
                return RedirectToAction("List");
            }
            else
            {
                _blogService.Update(entity);
                ViewData["title"] = "Update";
                TempData["action"] = "Update";
                return RedirectToAction("List");
            }


        }
    }

}
