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
    public class ProducerService : IProducerService
    {
        private readonly IProducerRepository _producerRepository;
        public ProducerService(IProducerRepository producerRepository)
        {
            _producerRepository = producerRepository;
        }
        public IList<ProducerResponse> Get()
        {
            IList<Producer> producer = _producerRepository.Get();
            return producer.Select(a => new ProducerResponse
            {
                Id = a.Id,
                Name = a.Name,
                Gender = a.Gender,
                DOB = a.DOB,
                Bio = a.Bio
            }).ToList();
        }
        public ProducerResponse Get(int id)
        {
            var producer = _producerRepository.Get(id);
            if (producer == null)
            {
                throw new Exception($"Producer with id {id} not found");
            }
            return new ProducerResponse { Id = id, Name = producer.Name, Bio = producer.Bio, DOB = producer.DOB, Gender = producer.Gender };
        }
        public void Create(ProducerRequest producerRequest)
        {
            if (string.IsNullOrEmpty(producerRequest.Name))
            {
                throw new Exception("Producer Name is required");
            }
            if (string.IsNullOrEmpty(producerRequest.Bio))
            {
                throw new Exception("Producer Bio is required");
            }
            if (string.IsNullOrEmpty(producerRequest.Gender))
            {
                throw new Exception("Producer Gender is required");
            }
            if (producerRequest.DOB.ToString("yyyy-MM-ddTHH:mm:ss") == "0001-01-01T00:00:00" || producerRequest.DOB == DateTime.MinValue || producerRequest.DOB > DateTime.Now)
            {
                throw new Exception("Enter valid DateTime");
            }
            _producerRepository.Create(producerRequest);
        }
        public void Update(int id,ProducerRequest producerRequest)
        {
            if (string.IsNullOrEmpty(producerRequest.Name))
            {
                throw new Exception("Producer Name is required");
            }
            if (string.IsNullOrEmpty(producerRequest.Bio))
            {
                throw new Exception("Producer Bio is required");
            }
            if (string.IsNullOrEmpty(producerRequest.Gender))
            {
                throw new Exception("Producer Gender is required");
            }
            if (producerRequest.DOB.ToString("yyyy-MM-ddTHH:mm:ss") == "0001-01-01T00:00:00" || producerRequest.DOB == DateTime.MinValue || producerRequest.DOB > DateTime.Now)
            {
                throw new Exception("Enter valid DateTime");
            }
            var producer = _producerRepository.Get(id);
            if (producer == null)
            {
                throw new ArgumentNullException($"Not Found");
            }
            _producerRepository.Update(id,producerRequest);
        }
        public void Delete(int id)
        {
            var producerResponse = _producerRepository.Get(id);
            if(producerResponse==null)
            {
                throw new ArgumentNullException($"Not Found");
            }
            _producerRepository.Delete(id);
        }
    }
}
