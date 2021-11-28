using AutoMapper;
using FashionShop.Models;
using FashionShop.Models.Dtos;
using FashionShop.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IUnitOfWork unitOfWork, ILogger<ProductController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts([FromQuery] RequestParams requestParams)
        {
            var products = await _unitOfWork.Products.GetAll(requestParams, include: q => q.Include(x => x.ProductSizes).ThenInclude(x => x.Size).Include(x => x.Color).Include(x => x.Style).ThenInclude(x => x.MaterialCategory).ThenInclude(i => i.Material).Include(x => x.Style).ThenInclude(x => x.MaterialCategory).ThenInclude(i => i.Category).Include(x => x.Style).ThenInclude(x => x.Gender));
            var result = _mapper.Map<IList<ProductDto>>(products);
            return Ok(result);

        }
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProduct(int id)
        {

            var product = await _unitOfWork.Products.Get(q => q.Id == id, include: q => q.Include(x => x.ProductSizes).ThenInclude(x =>x.Size).Include(x=>x.Color).Include(x => x.Style).ThenInclude(x => x.MaterialCategory).ThenInclude(i => i.Material).Include(x => x.Style).ThenInclude(x => x.MaterialCategory).ThenInclude(i => i.Category).Include(x => x.Style).ThenInclude(x => x.Gender));
            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);


        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto ProductDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Post Attempt in { nameof(CreateProduct)}");
                return BadRequest("Invalid Data Inserted");
            }

            var product = _mapper.Map<Product>(ProductDTO);
            await _unitOfWork.Products.Insert(product);
            await _unitOfWork.Save();

            return Created("GetProduct", product);


        }
        [Authorize (Roles = "Administrator")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto ProductDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Update Attempt in { nameof(UpdateProduct)}");
                return BadRequest("Invalid Data Inserted");
            }

            var product = await _unitOfWork.Products.Get(q => q.Id == id);
            if (product == null)
            {
                _logger.LogInformation($"Invalid Update Attempt in { nameof(UpdateProduct)}");
                return BadRequest("Invalid Data Inserted");
            }
            _mapper.Map(ProductDTO, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.Save();
            return NoContent();


        }
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteProduct)}");
                return BadRequest();
            }


            var product = await _unitOfWork.Products.Get(q => q.Id == id);
            if (product == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteProduct)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Products.Delete(id);
            await _unitOfWork.Save();

            return NoContent();

        }

    }
}
