using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Service;
using Microsoft.AspNetCore.Mvc;
using Assignment1.Models.Db;
using Assignment1.Models.Request;
using Assignment1.Models.Response;
namespace Assignment1.Controllers
{
    [ApiController]
    [Route("genres")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] GenreRequest genreRequest)
        {
         
            try
            {
                _genreService.Create(genreRequest);
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
                IList<GenreResponse> genres = _genreService.Get();
                return Ok(genres);
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
                GenreResponse genreResponse = _genreService.Get(id);
                if (genreResponse == null)
                {
                    return Ok(new List<GenreResponse>());
                }
                return Ok(genreResponse);
            }
            catch(Exception e)
            {
                return NotFound();
            }

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] GenreRequest genreRequest)
        {

            try
            {
                _genreService.Update(id, genreRequest);
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
                _genreService.Delete(id);
                return Ok();
            }
            catch (ArgumentNullException e)
            {
                return NotFound();
            }
        }
    }
}
