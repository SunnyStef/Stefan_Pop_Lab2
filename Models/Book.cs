
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stefan_Pop_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }

        [Display(Name = "Book Title")]
        public string Title { get; set; } = string.Empty;

        public int? AuthorsID { get; set; }
        public Authors? Authors { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }

        // Cheia straina pentru editura
        public int? PublisherID { get; set; }

        // Legatura cu editura
        public Publisher? Publisher { get; set; }
    }
}
