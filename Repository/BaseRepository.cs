
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Assignment.Repository
{
    public class BaseRepository<T> where T: class
    {
        private readonly string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<T> Get(string query)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<T>(query).ToList();
        }

        public T Get(string query , object parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QueryFirstOrDefault<T>(query, parameters);
        }

        public void Create(string query , T entity)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute(query, entity);
        }

        public void Update(string query , T entity)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute(query, entity);
        }

        public void Delete(string query, object parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute(query, parameters);
        }
    }
}
