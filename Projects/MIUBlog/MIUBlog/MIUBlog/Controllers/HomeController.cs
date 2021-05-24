using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MIUBlog.Business.Abstract;
using MIUBlog.DataAccess.Concrete.EntityFramework;
using MIUBlog.Models;
using MIUBlog.WebUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MIUBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IBlogService _blogService;
        public HomeController(ILogger<HomeController> logger, IBlogService blogService)
        {
            _logger = logger;
            _blogService = blogService;
        }



        public IActionResult Index()
        {
            HomeBlogModel model = new HomeBlogModel();
            model.HomeBlog = _blogService.GetAll().Where(i => i.isApproved && i.isHome).ToList();
            model.SliderBlog = _blogService.GetAll().Where(i => i.isApproved && i.isSlider).ToList();
            return View(model);
        }

        public IActionResult List()
        {
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
