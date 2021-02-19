using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsBoard.Services
{
    public interface IDataViewService
    {
        List<News> GetNews();
        List<NewsCategory> GetNewsCategories();
        SelectList CategorySelect();
        List<News> CategoryShow(int? id);
        News ShowNews(int? id);
        void CreateNews(News news, IFormFile uploadedFile);
        void Delete(News news);
        News EditNews(int? id);
        void ConfirmEditNews(News news, IFormFile uploadedFile);
        void CreateCategory(NewsCategory newsCategory);

    }
}
