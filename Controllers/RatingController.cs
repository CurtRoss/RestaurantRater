using ReestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ReestaurantRater.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();
        //Create new ratings
        // Post api/Rating
        [HttpPost]
        public async Task<IHttpActionResult> CreateRating([FromBody] Rating model)
        {
            // Check if model is null
            if (model is null)
                return BadRequest("You request body cannot be empty.");

            //Check if model state is invalid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // find the restaurant by the model.restaurantId and see that it exists
            var restaurantEntity = await _context.Restaurants.FindAsync(model.RestaurantId);
            if (restaurantEntity is null)
                return BadRequest($"The target restaurant with the ID of {model.RestaurantId} does not exist.");

            //Create the rating

            //Add to the rating table
            //_context.Ratings.Add(model);

            //Add to the restaurant entity
            restaurantEntity.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
                return Ok($"You rated restaurant {restaurantEntity.Name} successfully");

            return InternalServerError();
        }

        //Get a rating by its ID

        //Get All ratings

        //Get All Ratings for a specific restaurant by the restaurant ID

        //Update a rating

        //Delete a Rating
    }
}
