using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsBoard.Models;

namespace NewsBoard.Services
{
    public class DataViewService : IDataViewService
    {
        NewsContext db;
        IWebHostEnvironment newsEnvironment;

        public DataViewService(NewsContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            newsEnvironment = appEnvironment;
        }

        public List<News> GetNews()
        {
              return db.News.Include(c => c.NewsCategory).OrderByDescending(p => p.NewsDate).Take(6).ToList();
        }

        public List<NewsCategory> GetNewsCategories()
        {
            List<NewsCategory> category = db.NewsCategories.ToList();
            return category;
        }

        public List<News> CategoryShow(int? id)
        {
            return db.News.Include(c => c.NewsCategory).OrderByDescending(p => p.NewsDate).Where(p => p.NewsCategoryId == id).ToList();
        }
        
        public News ShowNews(int? id)
        {
            return db.News.Find(id);
        }

        public void CreateNews(News news,IFormFile uploadedFile)
        {
            string path = "/img/" + uploadedFile.FileName;
            using (var fileStream = new FileStream(newsEnvironment.WebRootPath+path, FileMode.Create))
            {
                uploadedFile.CopyTo(fileStream);
            }

            var addedNews = new News()
            {
                NewsName = news.NewsName,
                NewsText = news.NewsText,
                NewsDate = DateTime.Now,
                NewsCategoryId = news.NewsCategoryId,
                ImgName = uploadedFile.FileName
            };

            db.News.Add(addedNews);
            db.SaveChanges();
        }

        public void Delete(News news)
        {
            db.News.Remove(news);
            db.SaveChanges();
        }

        public void ConfirmEditNews(News news, IFormFile uploadedFile)
        {
            if(uploadedFile!=null)
            {
                string path = "/img/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(newsEnvironment.WebRootPath + path, FileMode.Create))
                {
                    uploadedFile.CopyTo(fileStream);
                }
            }
            var editedNews = db.News.Find(news.Id);
            if (uploadedFile != null) editedNews.ImgName = uploadedFile.FileName;
            if (news.NewsName != null && news.NewsName != editedNews.NewsName) editedNews.NewsName = news.NewsName;
            if (news.NewsText != null && news.NewsText != editedNews.NewsText) editedNews.NewsText = news.NewsText;
            if (news.NewsCategoryId != 0 && news.NewsCategoryId != editedNews.NewsCategoryId) editedNews.NewsCategoryId = news.NewsCategoryId;
            editedNews.NewsDate = DateTime.Now;
            db.Entry(editedNews).State = EntityState.Modified;
            db.SaveChanges();
        }

        public News EditNews(int? id)
        {
            var news = db.News.Find(id);
            News value = new News
            {
                Id = news.Id,
                NewsName = news.NewsName,
                NewsText = news.NewsText,
                NewsCategoryId = news.NewsCategoryId,
                ImgName=news.ImgName
                
            };
            return value;
        }

        public SelectList CategorySelect()
        {
            SelectList categories = new SelectList(db.NewsCategories, "Id", "CategoryName");
            return categories;
        }

    }
}
