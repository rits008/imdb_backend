using System;
using System.Collections.Generic;
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
    public class ProducerRepository : BaseRepository<Producer> ,IProducerRepository
    {
        private readonly ConnectionString _connectionString;
        public ProducerRepository(IOptions<ConnectionString> connectionString)
            :base(connectionString.Value.IMDBDB)
        {
            _connectionString = connectionString.Value;
        }

        public IList<Producer> Get()
        {
            const string query = @"
SELECT [id],
       [name],
       [sex] AS [Gender],
       [bio],
       [dob]
FROM   [foundation].[producers]";

            return Get(query);
        }
        public Producer Get(int id)
        {
            const string query = @"
SELECT [id],
       [name],
       [sex] AS [Gender],
       [bio],
       [dob]
FROM   [foundation].[producers] 
Where Id = @id";

            
            return Get(query, new { Id = id });
        }
        public void Create(ProducerRequest producerRequest)
        {
            const string query = @"
INSERT INTO [Foundation].[producers]
            ([name],
             [sex],
             [bio],
             [dob])
VALUES      (@Name,
             @Gender,
             @Bio,
             @DOB) ";

            Producer newProducer = new Producer { Name = producerRequest.Name, Bio = producerRequest.Bio, DOB = producerRequest.DOB, Gender = producerRequest.Gender };
            Create(query, newProducer);
        }
        public void Update(int id , ProducerRequest producerRequest)
        {
            const string query = @"
UPDATE [foundation].[producers]
SET    [Name] = @Name,
       [Sex] = @Gender,
       [Bio] = @Bio,
       [DOB] = @DOB
WHERE  [Id] = @id";
            Producer newProducer = new Producer { Id = id, Name = producerRequest.Name, Bio = producerRequest.Bio, DOB = producerRequest.DOB, Gender = producerRequest.Gender };
            Update(query, newProducer);
        }
        public void Delete(int id)
        {
            const string query = @"
DELETE [Foundation].[producers]
WHERE  id = @id";

            Delete(query, new { Id = id });
        }
    }
}
