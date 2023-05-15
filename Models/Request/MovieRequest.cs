using Assignment1.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1.Models.Request
{
    public class MovieRequest
    {
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public List<int> ActorsId { get; set; }
        public List<Genre> Genres { get; set; }
        public int ProducerID { get; set; }
        public string CoverImage { get; set; }
    }
}
