using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models.Db;
using Assignment1.Models.Request;
using Assignment1.Models.Response;
namespace Assignment1.Repository
{
    public interface IMovieRepository
    {
        IList<Movie> Get();
        Movie Get(int id);
        void Create(MovieRequest movieRequest, Producer producer);
        void Update(int id , MovieRequest movieRequest, Producer producer);
        void Delete(int id);
        List<int> GetAllActorId(int id);
        List<int> GetAllGenreId(int id);
    }
}
