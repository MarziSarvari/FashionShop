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
    public class ProductSizeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductSizeController> _logger;
        public ProductSizeController(IUnitOfWork unitOfWork, ILogger<ProductSizeController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductSizes([FromQuery] RequestParams requestParams)
        {
            var productSizes = await _unitOfWork.ProductSizes.GetAll(requestParams, include: q => q.Include(x => x.Size).Include(x => x.Product).ThenInclude(x => x.Color).Include(x => x.Product).ThenInclude(x => x.Style));
            var result = _mapper.Map<IList<ProductSizeDto>>(productSizes);
            return Ok(result);

        }
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductSize(int id)
        {

            var productSize = await _unitOfWork.ProductSizes.Get(q => q.Id == id, include: q => q.Include(x => x.Size).Include(x => x.Product).ThenInclude(x => x.Style).Include(x => x.Product).ThenInclude(x=>x.Color).Include(x => x.Product).ThenInclude(x => x.Style).ThenInclude(x => x.MaterialCategory).ThenInclude(i => i.Material).Include(x => x.Product).ThenInclude(x => x.Style).ThenInclude(x => x.MaterialCategory).ThenInclude(i => i.Category).Include(x => x.Product).ThenInclude(x => x.Style).ThenInclude(x => x.Gender));
            var result = _mapper.Map<ProductSizeDto>(productSize);
            return Ok(result);


        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProductSize([FromBody] CreateProductSizeDto ProductSizeDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Post Attempt in { nameof(CreateProductSize)}");
                return BadRequest("Invalid Data Inserted");
            }

            var productSize = _mapper.Map<ProductSize>(ProductSizeDTO);
            await _unitOfWork.ProductSizes.Insert(productSize);
            await _unitOfWork.Save();

            return Created("GetProductSize", productSize);


        }
        [Authorize (Roles = "Administrator")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductSize(int id, [FromBody] UpdateProductSizeDto ProductSizeDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Update Attempt in { nameof(UpdateProductSize)}");
                return BadRequest("Invalid Data Inserted");
            }

            var productSize = await _unitOfWork.ProductSizes.Get(q => q.Id == id);
            if (productSize == null)
            {
                _logger.LogInformation($"Invalid Update Attempt in { nameof(UpdateProductSize)}");
                return BadRequest("Invalid Data Inserted");
            }
            _mapper.Map(ProductSizeDTO, productSize);
            _unitOfWork.ProductSizes.Update(productSize);
            await _unitOfWork.Save();
            return NoContent();


        }
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProductSize(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteProductSize)}");
                return BadRequest();
            }


            var productSize = await _unitOfWork.ProductSizes.Get(q => q.Id == id);
            if (productSize == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteProductSize)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.ProductSizes.Delete(id);
            await _unitOfWork.Save();

            return NoContent();

        }

    }
}
