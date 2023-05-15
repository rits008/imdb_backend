using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment;
using Assignment.Repository;
using Assignment1.Models.Db;
using Assignment1.Models.Request;
using Assignment1.Models.Response;
using Dapper;
using Microsoft.Extensions.Options;

namespace Assignment1.Repository
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {

        private readonly string _connectionString;
        public MovieRepository(IOptions<ConnectionString> connectionString)
            :base(connectionString.Value.IMDBDB)
        {
            _connectionString = connectionString.Value.IMDBDB;
        }    
        
        public IList<Movie> Get()
        {
            const string query = @"
SELECT M.[Id],
       M.[Name],
       M.[ReleaseYear] as YearOfRelease,
       M.[Plot],
       M.[ProducerId],
       M.[PosterImage] as CoverImage
FROM   [foundation].[movies] M";

            return Get(query);

        }
        public Movie Get(int id)
        {
            const string query = @"
SELECT M.[Id],
       M.[Name],
       M.[ReleaseYear] as YearOfRelease,
       M.[Plot],
       M.[ProducerId],
       M.[PosterImage] as CoverImage
FROM   [foundation].[movies] M
WHERE  M.[id] = @id";

            Movie m = Get(query, new { Id = id });

            return  m;
        }
        public void Create(MovieRequest movieRequest,Producer producer)
        {
          
            using (var connection = new SqlConnection(_connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("chvMovieName", movieRequest.Name);
                parameters.Add("intReleaseYear",movieRequest.YearOfRelease);
                parameters.Add("txtPlot",movieRequest.Plot);

                
                parameters.Add("bivPosterImage",movieRequest.CoverImage);
                parameters.Add("intProducerID",movieRequest.ProducerID);

                string actorIDs = string.Join(",", movieRequest.ActorsId.Select(a=>a.ToString()));

                parameters.Add("chvActorID",actorIDs);

                string genreIDs = string.Join(",", movieRequest.Genres.Select(g => g.Id.ToString()));
                parameters.Add("chvGenreID", genreIDs);
                connection.Execute("usp_insert_movies", parameters, commandType: CommandType.StoredProcedure);
               
            }
            
        }
        public void Update(int id , MovieRequest movieRequest, Producer producer)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("intMovieID", id);
                parameters.Add("chvMovieName", movieRequest.Name);
                parameters.Add("intReleaseYear", movieRequest.YearOfRelease);
                parameters.Add("txtPlot", movieRequest.Plot);
                parameters.Add("bivPosterImage", movieRequest.CoverImage);
                parameters.Add("intProducerID", movieRequest.ProducerID);

                string actorIDs = string.Join(",", movieRequest.ActorsId.Select(a => a.ToString()));
                parameters.Add("chvActorID", actorIDs);

                string genreIDs = string.Join(",", movieRequest.Genres.Select(g => g.Id.ToString()));
                parameters.Add("chvGenreID", genreIDs);

                connection.Execute("usp_update_movie", parameters, commandType: CommandType.StoredProcedure);
            }

        }
        public void Delete(int id)
        {
            const string query = @"
DELETE FROM [Foundation].[actor_movies]
WHERE  movieid = @id;

DELETE FROM [Foundation].[genre_movies]
WHERE  movieid = @id;

DELETE FROM [Foundation].[movies]
WHERE  id = @id ";


            Delete(query, new { Id = id });
        }

        public List<int> GetAllActorId(int id)
        {
            const string query = @"
SELECT DISTINCT AM.[actorid]
FROM   [foundation].[movies] M
       INNER JOIN [foundation].[actor_movies] AM
               ON M.[id] = AM.[movieid]
WHERE  M.[id] = @id ";

            using var connection = new SqlConnection(_connectionString);
            List<int> a = (List<int>)connection.Query<int>(query, new { Id = id });

            return a;
        }

        public List<int> GetAllGenreId(int id)
        {
            const string query = @"
SELECT DISTINCT GM.genreid
FROM   [foundation].[movies] M
       INNER JOIN [foundation].[genre_movies] GM
               ON M.id = GM.movieid
WHERE  M.id = @id ";

            using var connection = new SqlConnection(_connectionString);
            List<int> g = (List<int>)connection.Query<int>(query, new { Id = id });
            return g;
        }
    }
}
