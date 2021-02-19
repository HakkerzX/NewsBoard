using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NewsBoard.Models
{
    public class NewsContext : DbContext
    {
        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<News> News { get; set; }

        public NewsContext(DbContextOptions<NewsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
