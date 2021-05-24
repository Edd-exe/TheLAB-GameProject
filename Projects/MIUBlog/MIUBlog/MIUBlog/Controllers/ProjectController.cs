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

namespace MIUBlog.WebUI.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectService _projectService;
        private ICategoryService _categoryService;
        private ICommentService _commentService;
        private IProjectUserService _projectUserService;
        private UserManager<ApplicationUser> _userManager;
        public ProjectController(IProjectService projectService,
            ICategoryService categoryService,
            ICommentService commentService,
            UserManager<ApplicationUser> userManager,
            IProjectUserService projectUserService
            )
        {
            _projectService = projectService;
            _categoryService = categoryService;
            _commentService = commentService;
            _userManager = userManager;
            _projectUserService = projectUserService;
        }



        public IActionResult Index()
        {
            var query = _projectService.GetAll().Where(i => i.isApproved);

            return View(query.OrderByDescending(i => i.Date));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult List()
        {
            return View(_projectService.GetAll());
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "User")]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "CategoryId", "Name");

            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> Create(Project entity)
        {
            TempData["action"] = "Create";


            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                entity.ApplicationUserId = user.Id;
                _projectService.Add(entity);
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

            return View(_projectService.GetAll().Where(i => i.Id == id).FirstOrDefault());

        }
        public IActionResult ProjectApplication(int id, string applicationUserId)
        {
            Project project = _projectService.Get(id);
            ProjectUser projectUser = _projectUserService.GetByProjectIdAndUserId(id, applicationUserId);
            if (projectUser == null)
            {
                ProjectUser prjUser = new ProjectUser
                {
                    ProjectId = id,
                    ApplicationUserId = applicationUserId
                };
                _projectUserService.Add(prjUser);
            }

            return RedirectToAction("Details", project);
        }
        public async Task<IActionResult> Details(int id, Project blg)
        {
            int projectId = id;
            if (id == 0)
            {
                projectId = blg.Id;
            }
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);


            ProjectUser projectUser = _projectUserService.GetByProjectIdAndUserId(projectId, user.Id);
            var project = _projectService.Get(projectId);
            var comments = _commentService.GetAll()
                .Include(i => i.ApplicationUser)
                .Where(i => i.Project.Id == projectId && i.isApproved == true);
            ProjectModel model = new ProjectModel()
            {
                Project = project,
                Comments = comments,
                Status = projectUser != null ? true : false
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Project entity, IFormFile file)
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


                _projectService.Update(entity);
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
            var result = _projectService.GetAll().Where(i => i.Id == id).FirstOrDefault();
            TempData["message"] = $"{result.Title } Silmek İstediğinizden Eminmisiniz";


            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmate(int ProjectId)
        {
            try
            {
                var result = _projectService.GetAll().Where(i => i.Id == ProjectId).FirstOrDefault();
                TempData["message"] = $"{result.Title} Silindi";
                if (ModelState.IsValid)
                {
                    _projectService.Delete(ProjectId);

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
        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "User")]
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
                return View("Create", _projectService.GetAll().FirstOrDefault(i => i.Id == id));

            }


        }


        public async Task<IActionResult> UserProjectList()
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            List<Project> projects = _projectService.GetByUserId(user.Id);

            return View(projects);
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> AddOrUpdate(Project entity, IFormFile file)
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

                _projectService.Add(entity);
                ViewData["title"] = "Create";
                TempData["action"] = "Create";
                if (User.IsInRole("User"))
                {
                    return RedirectToAction("UserProjectList");
                }
                return RedirectToAction("List");
            }
            else
            {
                _projectService.Update(entity);
                ViewData["title"] = "Update";
                TempData["action"] = "Update";

                if (User.IsInRole("User"))
                {
                    return RedirectToAction("UserProjectList");
                }
                return RedirectToAction("List");
            }


        }
        public IActionResult ParticipantList(int projectId)
        {
            List<ProjectUser> projectUsers = _projectUserService.GetByProjectId(projectId);
            var members = new List<ApplicationUser>();
            var nonmembers = new List<ApplicationUser>();

            foreach (var user in projectUsers)
            {
                var list = user.isApproved == true
                                ? members : nonmembers;
                list.Add(user.ApplicationUser);
            }
            Project project = _projectService.Get(projectId);
            var model = new ProjectDetails()
            {
                Project = project,
                Members = members,
                NonMembers = nonmembers
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ParticipantList(ProjectEditModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        ProjectUser projectUser = _projectUserService.GetByProjectIdAndUserId(model.ProjectId, userId);
                        if (projectUser != null)
                        {
                            projectUser.isApproved = true;
                            _projectUserService.Update(projectUser);
                        }
                    }
                }

                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        ProjectUser projectUser = _projectUserService.GetByProjectIdAndUserId(model.ProjectId, userId);
                        if (projectUser != null)
                        {
                            projectUser.isApproved = false;
                            _projectUserService.Update(projectUser);
                        }
                    }
                }
            }

            return RedirectToAction("UserProjectList");
        }

    }
}

