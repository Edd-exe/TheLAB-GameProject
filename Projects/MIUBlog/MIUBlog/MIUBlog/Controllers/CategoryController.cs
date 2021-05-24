using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIUBlog.Business.Abstract;
using MIUBlog.Entities.Concrete;
using MIUBlog.Models;

namespace TeknoFaydaBlog.WebUI.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class CategoryController:Controller
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View(_categoryService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category entity)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Add(entity);
                return RedirectToAction("List");
            }
            return View(entity);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_categoryService.GetAll().Where(i => i.CategoryId == id).FirstOrDefault());
        }
        [HttpPost]
        public IActionResult Edit(Category entity)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Update(entity);
                return RedirectToAction("List");
            }
            return View(entity);
        }
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _categoryService.GetAll().Where(i => i.CategoryId == id).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    TempData["message"] = $"{result.Name} Silindi.";
                    _categoryService.Delete(id);
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
    }
}
