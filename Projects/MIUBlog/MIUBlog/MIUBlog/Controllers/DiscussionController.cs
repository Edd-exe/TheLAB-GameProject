using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MIUBlog.Business.Abstract;
using MIUBlog.Entities.Concrete;
using MIUBlog.Models;
using MIUBlog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MIUDiscussion.WebUI.Controllers
{
    public class DiscussionController : Controller
    {
        private IDiscussionService _discussionService;
        private ICategoryService _categoryService;
        private ICommentService _commentService;
        private UserManager<ApplicationUser> _userManager;
        public DiscussionController(IDiscussionService discussionService, ICategoryService categoryService, ICommentService commentService, UserManager<ApplicationUser> userManager)
        {
            _discussionService = discussionService;
            _categoryService = categoryService;
            _commentService = commentService;
            _userManager = userManager;
        }



        public IActionResult Index()
        {
            var query = _discussionService.GetAll().Where(i => i.isApproved);
           
            return View(query.OrderByDescending(i => i.Date));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult List()
        {
            return View(_discussionService.GetAll());
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
        public async Task<IActionResult> Create(Discussion entity)
        {
            TempData["action"] = "Create";


            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                entity.ApplicationUserId = user.Id;
                _discussionService.Add(entity);
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

            return View(_discussionService.GetAll().Where(i => i.Id == id).FirstOrDefault());

        }

        public IActionResult Details(int id, Discussion blg)
        {
            int discussionId = id;
            if (id == 0)
            {
                discussionId = blg.Id;
            }


            var discussion = _discussionService.Get(discussionId);
            var comments = _commentService.GetAll()
                .Include(i => i.ApplicationUser)
                .Where(i => i.Discussion.Id == discussionId && i.isApproved == true);
            DiscussionModel model = new DiscussionModel()
            {
                Discussion = discussion,
                Comments = comments
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Discussion entity, IFormFile file)
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


                _discussionService.Update(entity);
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
            var result = _discussionService.GetAll().Where(i => i.Id == id).FirstOrDefault();
            TempData["message"] = $"{result.Title } Silmek İstediğinizden Eminmisiniz";


            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmate(int DiscussionId)
        {
            try
            {
                var result = _discussionService.GetAll().Where(i => i.Id == DiscussionId).FirstOrDefault();
                TempData["message"] = $"{result.Title} Silindi";
                if (ModelState.IsValid)
                {
                    _discussionService.Delete(DiscussionId);

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
                return View("Create", _discussionService.GetAll().FirstOrDefault(i => i.Id == id));

            }


        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddOrUpdate(Discussion entity, IFormFile file)
        {
            if (entity.Id == 0)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                entity.ApplicationUserId = user.Id;
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    entity.Image = file.FileName;
                }

                _discussionService.Add(entity);
                ViewData["title"] = "Create";
                TempData["action"] = "Create";
                return RedirectToAction("List");
            }
            else
            {
                _discussionService.Update(entity);
                ViewData["title"] = "Update";
                TempData["action"] = "Update";
                return RedirectToAction("List");
            }


        }
    }
}
