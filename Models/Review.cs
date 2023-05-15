using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1.Models.Db
{
    public class Review
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int MovieId { get; set; }
    }
}
