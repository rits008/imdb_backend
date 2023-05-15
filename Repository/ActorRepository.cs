using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
    
    public class ActorRepository : BaseRepository<Actor>, IActorRepository
    {

        
        private readonly ConnectionString _connectionString;
        public ActorRepository(IOptions<ConnectionString> connectionString)
            :base(connectionString.Value.IMDBDB)
        {
            _connectionString = connectionString.Value;
        }
       
        public IList<Actor> Get()
        {
            const string query = @"
SELECT [id],
       [name],
       [sex] AS [Gender],
       [bio],
       [dob]
FROM   [foundation].[actors]";

            return Get(query);
        }
        public Actor Get(int id)
        {
            const string query = @"
SELECT [id],
       [name],
       [sex] AS [Gender],
       [bio],
       [dob]
FROM   [foundation].[actors] 
Where Id = @id";

            return Get(query, new { Id = id });
        }
        public void Create(ActorRequest actorRequest)
        {
            const string query = @"
INSERT INTO [Foundation].[actors]
            ([name],
             [sex],
             [bio],
             [dob])
VALUES      (@Name,
             @Gender,
             @Bio,
             @DOB) ";

            Actor newActor = new Actor {Name = actorRequest.Name, Bio = actorRequest.Bio, DOB = actorRequest.DOB, Gender = actorRequest.Gender };
            Create(query, newActor);
        }
        public void Update(int id ,ActorRequest actorRequest)
        {
            const string query = @"
UPDATE [foundation].[actors]
SET    [Name] = @Name,
       [Sex] = @Gender,
       [Bio] = @Bio,
       [DOB] = @DOB
WHERE  [Id] = @id";

            Actor newActor = new Actor { Id = id, Name = actorRequest.Name, Bio = actorRequest.Bio, DOB = actorRequest.DOB, Gender = actorRequest.Gender };
            Update(query, newActor);
        }
        public void Delete(int id)
        {
            const string query = @"
DELETE [Foundation].[actors]
WHERE  id = @id";
           
            Delete(query, new { Id = id });

            
        }
    }
}
