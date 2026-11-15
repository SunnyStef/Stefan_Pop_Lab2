
using System.Collections.Generic;
using Stefan_Pop_Lab2.Models;

namespace Stefan_Pop_Lab2.ViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publisher> Publishers { get; set; }
            = new List<Publisher>();

        public IEnumerable<Book> Books { get; set; }
            = new List<Book>();
    }
}
