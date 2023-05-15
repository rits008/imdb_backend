using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models.Request;
using Assignment1.Models.Response;

namespace Assignment1.Service
{
    public interface IProducerService
    {
        IList<ProducerResponse> Get();
        ProducerResponse Get(int id);
        void Create(ProducerRequest producerRequest);
        void Update(int id,ProducerRequest producerRequest);
        void Delete(int id);
    }
}
