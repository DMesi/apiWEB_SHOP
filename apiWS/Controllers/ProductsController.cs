using apiWS.IRepository;
using apiWS.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  [Authorize]
  [ApiExplorerSettings(GroupName ="ProductsAPI")]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProductsController(IUnitOfWork unitOfWork, ILogger<AccountController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts()
        {          
                var products = await _unitOfWork.Products.GetAll();
                var results = _mapper.Map<List<ProductsDTO>>(products);  // korisnicima ce biti vidljivo samo ono sto je u LocationsDTO a ne u klasi Locations
                return Ok(results);
         }


        // "{id:int}" - template for get 
        //[Authorize(Roles = "User ")]
        //  [Authorize]
      
        [HttpGet("{id:int}", Name = "GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts(int id)
        {       
            var products = await _unitOfWork.Products.Get(q => q.Id == id);  
            var result = _mapper.Map<ProductsDTO>(products);
            return Ok(result);
       
        }

        [HttpGet("category/{category:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsByCategory(int category)
        {
            var products = await _unitOfWork.Products.GetAllByCategory(q => q.Id_categories == category);
            var result = _mapper.Map<List<ProductsDTO>>(products);
            return Ok(result);

        }

        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateProducts([FromBody] ProductsDTO productsDTO)
        {
            if (!ModelState.IsValid)
            {

                _logger.LogError($"Invalid POST atempt in {nameof(CreateProducts)}");
                return BadRequest(ModelState);

            }
          
                var products = _mapper.Map<Products>(productsDTO);
                await _unitOfWork.Products.Insert(products);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetProducts", new { id = products.Id }, products);   // da bi ovo radilo prikazalo novo kreiranu lokaciju moramo dodati  [HttpGet("{id:int}", Name = "GetLocation")]       
         
        }
        
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductsDTO productDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE atempt in {nameof(UpdateProduct)}");
                return BadRequest(ModelState);
            }
         
                var products = await _unitOfWork.Products.Get(q => q.Id == id);
                if (products == null)
                {
                    _logger.LogError($"Invalid UPDATE atempt in {nameof(UpdateProduct)}"); //ime kontrolera
                    return BadRequest("Submitted data is invalid");

                }
                _mapper.Map(productDTO, products);
                _unitOfWork.Products.Update(products);
                await _unitOfWork.Save();

                return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE atempt in {nameof(DeleteProduct)}");
                return BadRequest(ModelState);
            }
          
                var product = await _unitOfWork.Products.Get(q => q.Id == id);
                if (product == null)
                {
                    _logger.LogError($"Invalid DELETE atempt in {nameof(DeleteProduct)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Products.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
        
        }


        
        [HttpGet("categoryMeni")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public  IActionResult GetCategoryMeni()
        {
            var products =  _unitOfWork.Products.GetAllByCategoryMeni();
      
            return Ok(products);
        }








    }
}
