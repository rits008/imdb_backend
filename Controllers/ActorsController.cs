using Assignment1.Models.Response;
using Assignment1.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models.Db;
using Assignment1.Models.Request;
using Assignment1.Models.Response;

namespace Assignment1.Controllers
{
    [ApiController]
    [Route("actors")]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ActorRequest actorRequest)
        {
            try
            {
                _actorService.Create(actorRequest);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest();
            }
           
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IList<ActorResponse> actorResponses = _actorService.Get();
                return Ok(actorResponses);
            }
            catch(Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                ActorResponse actorResponse = _actorService.Get(id);
                if (actorResponse == null)
                {
                    return Ok(new List<ActorResponse>());
                }
                return Ok(actorResponse);
            }
            catch(Exception e)
            {
                return NotFound();
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id , [FromBody] ActorRequest actorRequest)
        {
            try 
            {
                _actorService.Update(id, actorRequest);
                return Ok();
            }
            catch(ArgumentNullException e)
            {
                return NotFound();
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _actorService.Delete(id);
                return Ok();
            }
            catch(ArgumentNullException e)
            {
                return NotFound();
            }
            
        }
    }
}
