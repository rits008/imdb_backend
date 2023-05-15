using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment;
using Assignment.Repository;
using Assignment1.Models.Db;
using Assignment1.Models.Request;
using Assignment1.Models.Response;
using Microsoft.Extensions.Options;

namespace Assignment1.Repository
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        private readonly ConnectionString _connectionString;
        public GenreRepository(IOptions<ConnectionString> connectionString)
            :base(connectionString.Value.IMDBDB)
        {
          
        }
        public IList<Genre> Get()
        {
            const string query = @"
SELECT [id],
       [name]
FROM   [foundation].[genres] ";
            return Get(query);
        }
        public Genre Get(int id)
        {
            const string query = @"
SELECT [id],
       [name]
FROM   [foundation].[genres]
Where Id = @id";
            return Get(query, new { Id = id });
        }
        public void Create(GenreRequest genreRequest)
        {
            const string query = @"
INSERT INTO [Foundation].[genres]
            ([name])            
VALUES      (@Name) ";

            Genre newGenre = new Genre { Name = genreRequest.Name};
            Create(query, newGenre);
        }
        public void Update(int id , GenreRequest genreRequest)
        {
            const string query = @"
UPDATE [Foundation].[genres]
SET    [Name] = @Name
WHERE  [Id] = @id";

            Genre newGenre = new Genre { Id = id, Name = genreRequest.Name};
            Update(query, newGenre);
        }
        public void Delete(int id)
        {
            const string query = @"
DELETE [Foundation].[genres]
WHERE  id = @id";


            Delete(query, new { Id = id });

        }
    }
}
