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
    public class SizeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SizeController> _logger;
        public SizeController(IUnitOfWork unitOfWork, ILogger<SizeController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSizes()
        {
          
            var sizes = await _unitOfWork.Sizes.GetAll(include: q => q.Include(x => x.ProductSizes));
            var result = _mapper.Map<IList<SizeDto>>(sizes);
            return Ok(result);

        }
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSize(int id)
        {

            var size = await _unitOfWork.Sizes.Get(q => q.Id == id, include: q => q.Include(x => x.ProductSizes));
            var result = _mapper.Map<SizeDto>(size);
            return Ok(result);


        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSize([FromBody] CreateSizeDto SizeDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Post Attempt in { nameof(CreateSize)}");
                return BadRequest("Invalid Data Inserted");
            }

            var size = _mapper.Map<Size>(SizeDTO);
            await _unitOfWork.Sizes.Insert(size);
            await _unitOfWork.Save();

            return Created("GetSize", size);


        }
        [Authorize (Roles = "Administrator")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSize(int id, [FromBody] UpdateSizeDto SizeDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Update Attempt in { nameof(UpdateSize)}");
                return BadRequest("Invalid Data Inserted");
            }

            var size = await _unitOfWork.Sizes.Get(q => q.Id == id);
            if (size == null)
            {
                _logger.LogInformation($"Invalid Update Attempt in { nameof(UpdateSize)}");
                return BadRequest("Invalid Data Inserted");
            }
            _mapper.Map(SizeDTO, size);
            _unitOfWork.Sizes.Update(size);
            await _unitOfWork.Save();
            return NoContent();


        }
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSize(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteSize)}");
                return BadRequest();
            }


            var size = await _unitOfWork.Sizes.Get(q => q.Id == id);
            if (size == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteSize)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Sizes.Delete(id);
            await _unitOfWork.Save();

            return NoContent();

        }

    }
}
