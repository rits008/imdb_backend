
using Assignment1.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models.Request;
using Assignment1.Models.Response;
using Assignment1.Models.Db;

namespace Assignment1.Service
{
    public class ActorService:IActorService
    {
        private readonly IActorRepository _actorRepository;
        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }
        public IList<ActorResponse> Get()
        {
            IList<Actor> actors = _actorRepository.Get();
            return actors.Select(a => new ActorResponse
            {
                Id = a.Id,
                Name = a.Name,
                Gender = a.Gender,
                DOB = a.DOB,
                Bio = a.Bio
            }).ToList();
        }
        public ActorResponse Get(int id)
        {
            var actor = _actorRepository.Get(id);
            if(actor==null)
            {
                throw new Exception($"Actor with id {id} not found");
            }
            return new ActorResponse { Id = id,Name = actor.Name , Bio = actor.Bio, DOB = actor.DOB, Gender=actor.Gender};
        }
        public void Create(ActorRequest actorRequest)
        {
            if(string.IsNullOrEmpty(actorRequest.Name))
            {
                throw new Exception("Actor Name is required");
            }
            if(string.IsNullOrEmpty(actorRequest.Bio))
            {
                throw new Exception("Actor Bio is required");
            }
            if(string.IsNullOrEmpty(actorRequest.Gender))
            {
                throw new Exception("Actor Gender is required");
            }
            if(actorRequest.DOB.ToString("yyyy-MM-ddTHH:mm:ss")== "0001-01-01T00:00:00" || actorRequest.DOB ==  DateTime.MinValue  || actorRequest.DOB > DateTime.Now )
            {
                throw new Exception("Enter valid DateTime");
            }

            _actorRepository.Create(actorRequest);
        }
        public void Update(int id,ActorRequest actorRequest)
        {
            if (string.IsNullOrEmpty(actorRequest.Name))
            {
                throw new Exception("Actor Name is required");
            }
            if (string.IsNullOrEmpty(actorRequest.Bio))
            {
                throw new Exception("Actor Bio is required");
            }
            if (string.IsNullOrEmpty(actorRequest.Gender))
            {
                throw new Exception("Actor Gender is required");
            }
            if (actorRequest.DOB.ToString("yyyy-MM-ddTHH:mm:ss") == "0001-01-01T00:00:00" || actorRequest.DOB == DateTime.MinValue || actorRequest.DOB > DateTime.Now)
            {
                throw new Exception("Enter valid DateTime");
            }
            var actor  = _actorRepository.Get(id);
            if (actor == null)
            {
                throw new ArgumentNullException($"Not Found");
            }
            _actorRepository.Update(id,actorRequest);
        }
        public void Delete(int id)
        {
            var actorResponse = _actorRepository.Get(id);
            if (actorResponse == null)
            {
                throw new ArgumentNullException($"Not Found");
            }
            _actorRepository.Delete(id);
        }
    }
}
