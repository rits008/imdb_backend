using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models.Request;
using Assignment1.Models.Response;

namespace Assignment1.Service
{
    public interface IMovieService
    {
        IList<MovieResponse> Get();
        MovieResponse Get(int id);
        void Create(MovieRequest movieRequest);
        void Update(int id,MovieRequest movieRequest);
        void Delete(int id);
    }
}
