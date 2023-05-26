using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using vecttorAPI.Models;
using vecttorAPI.Repositories;

namespace vecttorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsteroidsController : ControllerBase
    {
        private IRepositoryAsteroide repo;

        public AsteroidsController(IRepositoryAsteroide repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<List<Asteroide>> GetAsteroides([Required] String days)
        {
            List<Asteroide> misAsteroides;
            try
            {
                misAsteroides = repo.Top3AsteropidesPeligrosos(days);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


            return misAsteroides;
        }
    }
}
