﻿using Assignment1.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1.Models.Response
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Genre> Genres { get; set; }
        public Producer Producer { get; set; }
        public string CoverImage { get; set; }
    }
}
