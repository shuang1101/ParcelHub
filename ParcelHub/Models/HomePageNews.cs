using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class HomePageNews
    {
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public DateTime Post { get; set; }

        public int Id { get; set; }
    }
}
