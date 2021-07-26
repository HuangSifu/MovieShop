using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly ICastService _castService;
        public CastController(ICastService castService)
        {
            _castService = castService;
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCast(int id)    //error
        {
            var cast = await _castService.GetCastDetails(id);

            if (cast == null)
            {
                return NotFound($"No cast found for that {id}");
            }
            return Ok(cast);
        }
    }
}
