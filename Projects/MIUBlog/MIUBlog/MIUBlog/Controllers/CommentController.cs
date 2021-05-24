using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIUBlog.Business.Abstract;
using MIUBlog.Entities.Concrete;
using MIUBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIUBlog.WebUI.Controllers
{
    public class CommentController:Controller
    {
        private IBlogService _blogService;
        private IDiscussionService _discussionService;
        private IProjectService _projectService;
        private ICommentService _commentService;
        private UserManager<ApplicationUser> _userManager;

        public CommentController(IBlogService blogService,
            ICommentService commentService,
            UserManager<ApplicationUser> userManager,
            IDiscussionService discussionService,
            IProjectService projectService
            )
        {
            _blogService = blogService;
            _commentService = commentService;
            _userManager = userManager;
            _discussionService = discussionService;
            _projectService = projectService;
        }
        //[Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var comments = _commentService.GetAll().AsQueryable()
                .Include(i => i.Blog)
                .Include(i => i.ApplicationUser)
                .Include(i => i.Discussion)
                .Include(i => i.Project)
                .Where(i => i.isApproved == false);
            return View(comments);
        }
        
            public async Task<IActionResult> CreateProjectComment(int projectId, string commentText)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var project = _projectService.Get(projectId);
            Comment comment = new Comment();
            comment.Project = project;
            comment.ApplicationUser = user;
            comment.Date = DateTime.Now;
            comment.CommentText = commentText;
            _commentService.Add(comment);
            int id = project.Id;
            return RedirectToAction("Details", "Project", new Discussion() { Id = id });
        }
        public async Task<IActionResult> CreateDiscussionComment(int discussionId, string commentText)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var discussion = _discussionService.Get(discussionId);
            Comment comment = new Comment();
            comment.Discussion = discussion;
            comment.ApplicationUser = user;
            comment.Date = DateTime.Now;
            comment.CommentText = commentText;
            _commentService.Add(comment);
            int id = discussion.Id;
            return RedirectToAction("Details", "Discussion", new Discussion() { Id = id });
        }

        public async Task<IActionResult> Create(int blogId, string commentText)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var blog = _blogService.GetAll().FirstOrDefault(i => i.BlogId == blogId);
            Comment comment = new Comment();
            comment.Blog = blog;
            comment.ApplicationUser = user;
            comment.Date = DateTime.Now;
            comment.CommentText = commentText;
            _commentService.Add(comment);
            int id = blog.BlogId;
            return RedirectToAction("Details", "Blog", new Blog() { BlogId = blogId });
        }
        //[Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            var comment = _commentService.GetAll().AsQueryable()
                .Include(i => i.Blog)
                .Include(i => i.ApplicationUser)
                .Include(i => i.Discussion)
                .Include(i => i.Project)
                .FirstOrDefault(i => i.CommentId == id);
            return View(comment);
        }
        //[Authorize(Roles = "Admin")]
        public IActionResult isApprovedMessages()
        {
            var comments = _commentService.GetAll().AsQueryable()
                .Include(i => i.Blog)
                .Include(i => i.ApplicationUser)
                .Include(i =>i.Discussion)
                .Include(i => i.Project)
                .Where(i => i.isApproved == true);
            return View(comments);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult Update(int CommentId, bool isApproved)
        {
            var comment = _commentService.GetAll().FirstOrDefault(i => i.CommentId == CommentId);
            if (ModelState.IsValid)
            {
                comment.isApproved = isApproved;
                _commentService.Update(comment);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Güncelleme işlemi yapılamadı");
            }

            return View(comment);


        }
        //[Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _commentService.Delete(id);
                return RedirectToAction("Index");
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
    }
}
