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
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        
        private readonly ConnectionString _connectionString;
        public ReviewRepository(IOptions<ConnectionString> connectionString)
            :base(connectionString.Value.IMDBDB)
        {
            
        }
        public IList<Review> Get()
        {
            const string query = @"
SELECT [Id] , [Message], [MovieId] from [foundation].[Reviews]";
            return Get(query);
        }
        public Review Get(int id)
        {
            const string query = @"
SELECT [id],
       [Message],
       [MovieId]
FROM   [foundation].[Reviews] 
Where Id = @id";
            return Get(query, new { Id = id });
        }
        public void Create(ReviewRequest reviewRequest)
        {
            const string query = @"
INSERT INTO [Foundation].[reviews]
            ([message],
             [movieId])
VALUES      (@Message,
             @MovieId
             ) ";

            Review newReview = new Review { Message = reviewRequest.Message,MovieId = reviewRequest.MovieId};
            Create(query, newReview);
        }
        public void Update(int id , int movieId , ReviewRequest reviewRequest)
        {
            const string query = @"
UPDATE [foundation].[reviews]
SET    [Message] = @Message,
       [movieId] = @MovieId
WHERE  [Id] = @id";

            Review newReview = new Review { Id = id, Message = reviewRequest.Message,MovieId=reviewRequest.MovieId };
            Update(query, newReview);

        }
        public void Delete(int id)
        {
            const string query = @"
DELETE [Foundation].[reviews]
WHERE  id = @id";


            Delete(query, new { Id = id });
        }
    }
}
