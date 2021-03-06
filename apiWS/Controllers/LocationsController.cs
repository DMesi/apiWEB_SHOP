using apiWS.IRepository;
using apiWS.Models;
using AutoMapper;
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
     [Route("api/[controller]")]
    //[Route("api/loc")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "LocationAPI")]
    public class LocationsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public LocationsController(IUnitOfWork unitOfWork, ILogger<AccountController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLocations()
      //  public async Task<IActionResult> GetLocations([FromQuery]RequestParams requestParams)  //uzimamo parametre iz URL PAGGIN
        {
            try
            {
                var locations = await _unitOfWork.Locations.GetAll();
              //  var locations = await _unitOfWork.Locations.GetPagedList(requestParams);
                var results = _mapper.Map<List<LocationsDTO>>(locations);  // korisnicima ce biti vidljivo samo ono sto je u LocationsDTO a ne u klasi Locations
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wronk un the {nameof(GetLocations)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }

        }


        // "{id:int}" - template for get 
        //[Authorize(Roles = "User ")]
        [Authorize]
        [HttpGet("{id:int}", Name = "GetLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLocation(int id)
        {
            try
            {
                var locationss = await _unitOfWork.Locations.Get(q => q.Id == id, new List<string> { "Products" });  // include  new List<string > {"Products" } opciono
                var result = _mapper.Map<LocationsDTO>(locationss);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wronk un the {nameof(GetLocation)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }

        }



     //   [Authorize(Roles = "Administrator ")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateLocation([FromBody] LocationsCreateDTO locationsDTO)
        {
            if (!ModelState.IsValid)
            {

               _logger.LogError($"Invalid POST atempt in {nameof(CreateLocation)}");
                return BadRequest(ModelState);

            }

            try
            {
                var lokacija = _mapper.Map<Locations>(locationsDTO);
                await _unitOfWork.Locations.Insert(lokacija);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetLocation", new { id = lokacija.Id }, lokacija);   // da bi ovo radilo prikazalo novo kreiranu lokaciju moramo dodati  [HttpGet("{id:int}", Name = "GetLocation")]

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(CreateLocation)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] LocationsCreateDTO locationDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE atempt in {nameof(UpdateCountry)}");
                return BadRequest(ModelState);
            }
            try
            {
                var loc = await _unitOfWork.Locations.Get(q => q.Id == id);
                if (loc == null)
                {
                    _logger.LogError($"Invalid UPDATE atempt in {nameof(UpdateCountry)}"); //ime kontrolera
                    return BadRequest("Submitted data is invalid");

                }
                _mapper.Map(locationDTO, loc);
                _unitOfWork.Locations.Update(loc);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(UpdateCountry)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE atempt in {nameof(DeleteLocation)}");
                return BadRequest(ModelState);
            }
            try
            {
                var country = await _unitOfWork.Locations.Get(q => q.Id == id);
                if (country == null)
                {
                    _logger.LogError($"Invalid DELETE atempt in {nameof(DeleteLocation)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Locations.Delete(id);
                await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(DeleteLocation)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
    }
}
