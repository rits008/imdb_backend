using Assignment1.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models.Response;

namespace Assignment1.Service
{
    public interface IActorService
    {
        IList<ActorResponse> Get();
        ActorResponse Get(int id);
        void Create(ActorRequest actorRequest);
        void Update(int id,ActorRequest actorRequest);
        void Delete(int id);
    }
}
