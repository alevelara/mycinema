using Microsoft.AspNetCore.Mvc;
using Mycinema.Application.Constants.Routes;
using Mycinema.Application.Models.DTOs.Entities;
using System.Net;

namespace Mycinema.API.Controllers
{
    [ApiController]
    [Route(Routes.APIController)]
    public class MovieController : ControllerBase
    {
        public MovieController()
        {

        }

        [HttpGet(MovieRoutes.GetAllMovies)]        
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<MovieDto>>> GetAllMovies()
	    {
            var movies = new List<MovieDto>()
            {
                new MovieDto()
                {
                    Adult = false,
                    Id = 1,
                    OriginalLanguage = "en",
                    OriginalTitle = "cars",
                    ReleaseDate = DateTime.Now.AddDays(1)
                }
            };
		    return Ok(movies);
	    }

        [HttpGet(MovieRoutes.GetById)]        
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<MovieDto>> GetById([FromQuery]int Id)
        {
            var movies = new List<MovieDto>()
            {
                new MovieDto()
                {
                    Adult = false,
                    Id = 1,
                    OriginalLanguage = "en",
                    OriginalTitle = "cars",
                    ReleaseDate = DateTime.Now.AddDays(1)
                }
            };
            return Ok(movies.FirstOrDefault(m => m.Id == Id));
        }

    }
}