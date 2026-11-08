
using System.Collections.Generic;

namespace Stefan_Pop_Lab2.Models
{
    public class Publisher
    {
        public int ID { get; set; }

        public string PublisherName { get; set; } = string.Empty;

        // Cartile asociate editurii
        public ICollection<Book>? Books { get; set; }
    }
}
