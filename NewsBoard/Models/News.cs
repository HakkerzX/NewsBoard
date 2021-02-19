using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsBoard.Models
{
    public class News
    {
        public int Id { get; set; }
        public string NewsName { get; set; }
        public string NewsText { get; set; }
        public DateTime NewsDate { get; set; }
        public string ImgName { get; set; }

        public int NewsCategoryId { get; set; }
        public NewsCategory NewsCategory { get; set; }
    }
}
