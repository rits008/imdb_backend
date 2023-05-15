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
    [Route("movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpPost]
        public IActionResult Create([FromBody] MovieRequest movieRequest)
        {
            
            try
            {
                _movieService.Create(movieRequest);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IList<MovieResponse> movies = _movieService.Get();
                return Ok(movies);
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
                MovieResponse movieResponse = _movieService.Get(id);
                if (movieResponse == null)
                {
                    return Ok(new List<MovieResponse>());
                }
                return Ok(movieResponse);
            }
            catch(Exception e) 
            { 
                return NotFound(); 
            } 
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id , [FromBody] MovieRequest movieRequest)
        {
            try
            {
                _movieService.Update(id, movieRequest);
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
                _movieService.Delete(id);
                return Ok();
            }
            catch(ArgumentNullException e)
            {
                return NotFound();
            }
        }
    }
}
