using NewsBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsBoard
{
    public class SampleData
    {
        public static void Initialize(NewsContext context)
        {
            if (!context.NewsCategories.Any())
            {
                context.NewsCategories
                    .AddRange(
                    new NewsCategory
                    {
                        CategoryName = "Общее"
                    },
                    new NewsCategory
                    {
                        CategoryName = "Технологии"
                    },
                    new NewsCategory
                    {
                        CategoryName = "Спорт"
                    },
                    new NewsCategory
                    {
                        CategoryName = "Политика"
                    }
                    );
                context.SaveChanges();
            }

            if (!context.News.Any())
            {
                context.News
                    .AddRange(
                    new News
                    {
                        NewsName = "Мы открылись !",
                        NewsText = "Это первая новость на нашем сайте. УРА!!!",
                        NewsDate = new DateTime(2021, 2, 20, 18, 30, 25),
                        NewsCategoryId = 1,
                        ImgName = "1.jpg"
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
