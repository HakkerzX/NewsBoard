using Microsoft.AspNetCore.Mvc;
using NewsBoard.Models;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NewsBoard.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NewsBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataViewService _dataViewService;

        public HomeController(IDataViewService dataViewService)
        {
            _dataViewService = dataViewService;
        }

        public IActionResult Index()
        {
            ViewBag.Category = _dataViewService.GetNewsCategories();
            return View(_dataViewService.GetNews());
        }

        public IActionResult Create()
        {
            ViewBag.Category = _dataViewService.GetNewsCategories();
            return View();
        }

        [HttpGet]
        public IActionResult Category(int? id)
        {
            ViewBag.Category = _dataViewService.GetNewsCategories();
            if (id == null) return RedirectToAction("Index");
            return View(_dataViewService.CategoryShow(id));

        }
        [HttpPost]
        public IActionResult Create(News news, IFormFile uploadedFile)
        {

            ViewBag.Category = _dataViewService.GetNewsCategories();
            _dataViewService.CreateNews(news, uploadedFile);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult News(int? id)
        {
            ViewBag.Category = _dataViewService.GetNewsCategories();
            if(id==null)
            {
                return RedirectToAction("Index");
            }
            var newsView = _dataViewService.ShowNews(id);
            if(newsView==null)
            {
                return RedirectToAction("Index");
            }    
            return View(newsView);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            News news = _dataViewService.ShowNews(id);
            if(news== null) return RedirectToAction("Index");
            ViewBag.Category = _dataViewService.GetNewsCategories();
            return View(news);
        } 
        [HttpPost, ActionName ("Delete")]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            News news = _dataViewService.ShowNews(id);
            if (news == null) return RedirectToAction("Index");
            ViewBag.Category = _dataViewService.GetNewsCategories();
            _dataViewService.Delete(news);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Category = _dataViewService.GetNewsCategories();
            News news = _dataViewService.ShowNews(id);
            if(news!=null)
            {
                ViewBag.Categories = _dataViewService.CategorySelect();
                return View(news);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(News news, IFormFile uploadedFile)
        {
            _dataViewService.ConfirmEditNews(news, uploadedFile);
            return RedirectToAction("Index");
        }

        public IActionResult CreateCategory()
        {
            ViewBag.Category = _dataViewService.GetNewsCategories();
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(NewsCategory newsCategory)
        {
            _dataViewService.CreateCategory(newsCategory);
            return RedirectToAction("Index");
        }


    }
}
