using System;
using Assignment1.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assignment1.Models.Db;
using Assignment1.Models.Request;
using Assignment1.Models.Response;

namespace Assignment1.Controllers
{
    [ApiController]
    [Route("producers")]
    public class ProducersController : ControllerBase
    {
      
        private readonly IProducerService _producerService;
        public ProducersController(IProducerService producerService)
        {
            _producerService = producerService;
        }

        [HttpPost]
        public IActionResult Create([FromBody]  ProducerRequest producerRequest)
        {
            try
            {
                _producerService.Create(producerRequest);
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
                IList<ProducerResponse> producers = _producerService.Get();
                return Ok(producers);
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
                ProducerResponse producerResponse = _producerService.Get(id);
                if (producerResponse == null)
                {
                    return Ok(new List<ProducerResponse>());
                }
                return Ok(producerResponse);
            }
            catch(Exception e)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProducerRequest producerRequest)
        {
            try
            {
                _producerService.Update(id, producerRequest);
                return Ok();
            }
            catch (ArgumentNullException e)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
          
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _producerService.Delete(id);
                return Ok();
            }
            catch (ArgumentNullException e)
            {
                return NotFound();
            }

        }
    }
}
