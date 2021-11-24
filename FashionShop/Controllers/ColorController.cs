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
    public class ColorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ColorController> _logger;
        public ColorController(IUnitOfWork unitOfWork, ILogger<ColorController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetColors()
        {
          
            var colors = await _unitOfWork.Colors.GetAll(include: q => q.Include(x => x.Products));
            var result = _mapper.Map<IList<ColorDto>>(colors);
            return Ok(result);

        }
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetColor(int id)
        {

            var color = await _unitOfWork.Colors.Get(q => q.Id == id, include: q => q.Include(x => x.Products));
            var result = _mapper.Map<ColorDto>(color);
            return Ok(result);


        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateColor([FromBody] CreateColorDto colorDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Post Attempt in { nameof(CreateColor)}");
                return BadRequest("Invalid Data Inserted");
            }

            var color = _mapper.Map<Color>(colorDTO);
            await _unitOfWork.Colors.Insert(color);
            await _unitOfWork.Save();

            return Created("GetColor", color);


        }
        [Authorize (Roles = "Administrator")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateColor(int id, [FromBody] UpdateColorDto colorDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Update Attempt in { nameof(UpdateColor)}");
                return BadRequest("Invalid Data Inserted");
            }

            var color = await _unitOfWork.Colors.Get(q => q.Id == id);
            if (color == null)
            {
                _logger.LogInformation($"Invalid Update Attempt in { nameof(UpdateColor)}");
                return BadRequest("Invalid Data Inserted");
            }
            _mapper.Map(colorDTO, color);
            _unitOfWork.Colors.Update(color);
            await _unitOfWork.Save();
            return NoContent();


        }
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteColor(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteColor)}");
                return BadRequest();
            }


            var color = await _unitOfWork.Colors.Get(q => q.Id == id);
            if (color == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteColor)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Colors.Delete(id);
            await _unitOfWork.Save();

            return NoContent();

        }

    }
}
