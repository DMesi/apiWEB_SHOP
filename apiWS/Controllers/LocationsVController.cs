using apiWS.IRepository;
using apiWS.Models;
using AutoMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace apiWS.Controllers
{
    [ApiVersion("2.0", Deprecated =true)]
    //[Route("api/[controller]")]
    //[Route("api/loc")]
    // [ResponseCache(Duration =60)]  // 60s
   // [ResponseCache(CacheProfileName = "120SecondsDuration")]
   [HttpCacheExpiration(CacheLocation =CacheLocation.Public, MaxAge =60)]
   [HttpCacheValidation(MustRevalidate =false)]
    [Route("api/{v:apiversion}/loc")]
    [ApiController]
    public class LocationsVController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public LocationsVController(IUnitOfWork unitOfWork, ILogger<AccountController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetLocations()
        {
            var locations = await _unitOfWork.Locations.GetAll();
            // var locations = await _unitOfWork.Locations.GetPagedList(requestParams);
            var results = _mapper.Map<List<LocationsCreateDTO>>(locations);  // korisnicima ce biti vidljivo samo ono sto je u LocationsDTO a ne u klasi Locations
            return Ok(results);
        }
    }
}
