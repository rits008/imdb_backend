using Assignment1.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models.Request;
using Assignment1.Models.Response;
using Assignment1.Models.Db;

namespace Assignment1.Service
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        public  GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public IList<GenreResponse> Get()
        {
            IList<Genre> genres = _genreRepository.Get();
            return genres.Select(g => new GenreResponse
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();
        }
        public GenreResponse Get(int id)
        {
            var genre = _genreRepository.Get(id);
            if (genre == null)
            {
                throw new Exception($"Genre with id {id} not found");
            }
            return new GenreResponse { Id = id, Name = genre.Name };
        }
        public void Create(GenreRequest genreRequest)
        {
            if(string.IsNullOrEmpty(genreRequest.Name))
            {
                throw new Exception("Genre is required");
            }
            _genreRepository.Create(genreRequest);
        }
        public void Update(int id,GenreRequest genreRequest)
        {
            if(string.IsNullOrEmpty(genreRequest.Name))
            {
                throw new Exception("Genre is required");
            }
            var genre = _genreRepository.Get(id);
            if (genre == null)
            {
                throw new ArgumentNullException($"Not Found");
            }
            _genreRepository.Update(id,genreRequest);
        }
        public void Delete(int id)
        {
            var genreResponse = _genreRepository.Get(id);
            if(genreResponse==null)
            {
                throw new ArgumentNullException($"Not Found");
            }
            _genreRepository.Delete(id);
        }
    }
}
