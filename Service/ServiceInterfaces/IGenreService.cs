using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models.Request;
using Assignment1.Models.Response;

namespace Assignment1.Service
{
    public interface IGenreService
    {
        IList<GenreResponse> Get();
        GenreResponse Get(int id);
        void Create(GenreRequest genreRequest);
        void Update(int id,GenreRequest genreRequest);
        void Delete(int id);
    }
}
