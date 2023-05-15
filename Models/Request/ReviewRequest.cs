using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1.Models.Request
{
    public class ReviewRequest
    {
        public string Message { get; set; }
        public int MovieId { get; set; }
    }
}
