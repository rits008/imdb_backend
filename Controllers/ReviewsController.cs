using System;
using System.Collections.Generic;
using System.Linq;
using Assignment1.Service;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assignment1.Models.Db;
using Assignment1.Models.Request;
using Assignment1.Models.Response;

namespace Assignment1.Controllers
{
    [ApiController]
    [Route("/movies/{movieId}/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ReviewRequest reviewRequest)
        {
            try
            {
                _reviewService.Create(reviewRequest);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }    
            
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IList<ReviewResponse> reviews = _reviewService.Get();
                return Ok(reviews);
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
                ReviewResponse reviewResponse = _reviewService.Get(id);
                if (reviewResponse == null)
                {
                    return Ok(new List<ReviewResponse>());
                }
                return Ok(reviewResponse);
            }
            catch (Exception e) 
            { 
                return NotFound(); 
            } 
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, int movieId, [FromBody] ReviewRequest reviewRequest)
        {
            try
            {
                _reviewService.Update(id, movieId, reviewRequest);
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
                _reviewService.Delete(id);
                return Ok();
            }
            catch(ArgumentNullException e)
            {
                return NotFound();
            }

        }
    }
}
