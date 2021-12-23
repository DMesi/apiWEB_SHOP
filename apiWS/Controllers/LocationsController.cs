using apiWS.IRepository;
using apiWS.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
      //  private readonly ILogger _logger;

        public LocationsController(IUnitOfWork unitOfWork,  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
       //     _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLocations()
        {
            try
            {
                var locations = await _unitOfWork.Locations.GetAll();
               
                var results = _mapper.Map<List<LocationsDTO>>(locations);  // korisnicima ce biti vidljivo samo ono sto je u LocationsDTO a ne u klasi Locations
                return Ok(results);
            }
            catch (Exception ex)
            {
         //       _logger.LogError(ex, $"Something Went Wronk un the {nameof(GetLocations)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }

        }


        // "{id:int}" - template for get 
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var locationss = await _unitOfWork.Locations.Get(q => q.Id == id, new List<string> { "Products" });  // include  new List<string > {"Hotels" } opciono
                var result = _mapper.Map<LocationsDTO>(locationss);
                return Ok(result);
            }
            catch (Exception ex)
            {
              //  _logger.LogError(ex, $"Something Went Wronk un the {nameof(GetCountry)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }

        }






    }
}
