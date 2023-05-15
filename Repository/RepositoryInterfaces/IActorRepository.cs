using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models.Db;
using Assignment1.Models.Request;
using Assignment1.Models.Response;

namespace Assignment1.Repository
{
    public interface IActorRepository
    {
        IList<Actor> Get();
        Actor Get(int id);
        void Create(ActorRequest actorRequest);
        void Update(int id,ActorRequest actorRequest);
        void Delete(int id);

    }
}
