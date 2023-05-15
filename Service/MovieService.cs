using System;
using Assignment1.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models.Request;
using Assignment1.Models.Response;
using Assignment1.Models.Db;

namespace Assignment1.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IProducerService _producerService;
        private readonly IActorService _actorService;
        private readonly IGenreService _genreService;
        public MovieService(IMovieRepository movieRepository , IProducerService producerService, IActorService actorService, IGenreService genreService)
        {
            _movieRepository = movieRepository;
            _producerService = producerService;
            _actorService = actorService;
            _genreService = genreService;
        }
        public IList<MovieResponse> Get()
        {
            IList<Movie> movies = _movieRepository.Get();
            IList<MovieResponse> allMovies = new List<MovieResponse>();
            foreach(var movie in movies)
            {
                MovieResponse movieResponse = new MovieResponse();

                movieResponse.Name = movie.Name;
                movieResponse.Id = movie.Id;
                movieResponse.Plot = movie.Plot;
                movieResponse.CoverImage = movie.CoverImage;
                movieResponse.YearOfRelease = movie.YearOfRelease;
                movieResponse.CoverImage = movie.CoverImage;

                var producer = _producerService.Get(movie.ProducerId);
                Producer newProducer = new Producer { Id = movie.ProducerId, Name = producer.Name, Bio = producer.Bio, DOB = producer.DOB, Gender = producer.Gender };
                movieResponse.Producer = newProducer;


                List<Actor> allActorInMovie = new List<Actor>();
                List<int> ActorIDs = _movieRepository.GetAllActorId(movie.Id);
                foreach (var a in ActorIDs)
                {
                    var actorResponse = _actorService.Get(a);
                    Actor newActor = new Actor { Id = a, Name = actorResponse.Name, Bio = actorResponse.Bio, DOB = actorResponse.DOB, Gender = actorResponse.Gender };
                    allActorInMovie.Add(newActor);
                }
                movieResponse.Actors = allActorInMovie;


                List<int> GenreIDs = _movieRepository.GetAllGenreId(movie.Id);
                List<Genre> allGenreOfMovie = new List<Genre>();
                foreach (var g in GenreIDs)
                {
                    var genreResponse = _genreService.Get(g);
                    Genre newGenre = new Genre { Id = g, Name = genreResponse.Name };
                    allGenreOfMovie.Add(newGenre);
                }
                movieResponse.Genres = allGenreOfMovie;

                allMovies.Add(movieResponse);
            }
            return allMovies;
        }
        public MovieResponse Get(int id)
        {
            var movie = _movieRepository.Get(id);
            if(movie==null)
            {
                throw new Exception($"Movie with {id} is not present");
            }
            int producerId = movie.ProducerId;
            var producer = _producerService.Get(producerId);
            Producer newProducer = new Producer { Id = producerId, Name = producer.Name, Bio = producer.Bio, DOB = producer.DOB, Gender = producer.Gender };

            List<Actor> allActorInMovie = new List<Actor>();
            List<int> ActorIDs = _movieRepository.GetAllActorId(id);
            foreach(var a in ActorIDs)
            {
                var actorResponse = _actorService.Get(a);
                Actor newActor = new Actor { Id = a, Name = actorResponse.Name, Bio = actorResponse.Bio, DOB = actorResponse.DOB, Gender = actorResponse.Gender };
                allActorInMovie.Add(newActor);
            }


            List<int> GenreIDs = _movieRepository.GetAllGenreId(id);
            List<Genre> allGenreOfMovie = new List<Genre>();
            foreach(var g in GenreIDs)
            {
                var genreResponse = _genreService.Get(g);
                Genre newGenre = new Genre { Id = g, Name = genreResponse.Name };
                allGenreOfMovie.Add(newGenre);
            }
           
            return new MovieResponse { Id = id, Name = movie.Name, Plot = movie.Plot,Producer=newProducer, YearOfRelease = movie.YearOfRelease,Actors=allActorInMovie,Genres=allGenreOfMovie,CoverImage=movie.CoverImage};
        }
        public void Create(MovieRequest movieRequest)
        {

            if(string.IsNullOrEmpty(movieRequest.Name))
            {
                throw new Exception("Movie is Required");
            }
            if(string.IsNullOrEmpty(movieRequest.Plot))
            {
                throw new Exception("Movie Plot is Required");
            }
            if(movieRequest.ProducerID<0 || string.IsNullOrEmpty(movieRequest.ProducerID.ToString()))
            {
                throw new Exception("Movie Producer is required");
            }
            if(movieRequest.YearOfRelease<=0)
            {
                throw new Exception("Enter valid Release Year");
            }
            if(movieRequest.ActorsId.Count==0)
            {
                throw new Exception("Actors is Required");
            }
           
            if(movieRequest.Genres.Count==0)
            {
                throw new Exception("Genres is Required");
            }

            if (string.IsNullOrEmpty(movieRequest.CoverImage))
            {
                throw new Exception("Image is Required");
            }
            var producer = _producerService.Get(movieRequest.ProducerID);
            Producer newProducer = new Producer { Id = producer.Id, Name = producer.Name, Bio = producer.Bio, DOB = producer.DOB, Gender = producer.Gender };
            _movieRepository.Create(movieRequest,newProducer);
        }
        public void Update(int id,MovieRequest movieRequest)
        {
            if (string.IsNullOrEmpty(movieRequest.Name))
            {
                throw new Exception("Movie is Required");
            }
            if (string.IsNullOrEmpty(movieRequest.Plot))
            {
                throw new Exception("Movie Plot is Required");
            }
            if (movieRequest.ProducerID < 0)
            {
                throw new Exception("Movie Producer is required");
            }
            if (movieRequest.YearOfRelease <= 0)
            {
                throw new Exception("Enter valid Release Year");
            }
            if (movieRequest.ActorsId.Count == 0)
            {
                throw new Exception("Actors is Required");
            }
            if (movieRequest.Genres.Count == 0)
            {
                throw new Exception("Genres is Required");
            }
            if (movieRequest.CoverImage == null)
            {
                throw new Exception("Image is Required");
            }

            var movie = _movieRepository.Get(id);
            if (movie == null)
            {
                throw new ArgumentNullException($"Not Found");
            }

            var producer = _producerService.Get(movieRequest.ProducerID);
            Producer newProducer = new Producer { Id = producer.Id, Name = producer.Name, Bio = producer.Bio, DOB = producer.DOB, Gender = producer.Gender };

            _movieRepository.Update(id,movieRequest,newProducer);
        }
        public void Delete(int id)
        {
            var movieResponse = _movieRepository.Get(id);
            if(movieResponse==null)
            {
                throw new ArgumentNullException($"Not Found");
            }
            _movieRepository.Delete(id);
        }
    }
}
