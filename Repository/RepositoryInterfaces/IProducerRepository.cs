using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models.Db;
using Assignment1.Models.Request;
using Assignment1.Models.Response;

namespace Assignment1.Repository
{
    public interface IProducerRepository
    {
        IList<Producer> Get();
        Producer Get(int id);
        void Create(ProducerRequest producerRequest);
        void Update(int id , ProducerRequest producerRequest);
        void Delete(int id);
    }
}
