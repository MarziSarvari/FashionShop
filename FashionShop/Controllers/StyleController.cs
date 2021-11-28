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
    public class StyleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<StyleController> _logger;
        public StyleController(IUnitOfWork unitOfWork, ILogger<StyleController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStyles([FromQuery] RequestParams requestParams)
        {
          
            var styles = await _unitOfWork.Styles.GetAll(requestParams, include: q => q.Include(x => x.Gender).Include(x => x.Products).Include(x => x.MaterialCategory).ThenInclude(i => i.Material).Include(x => x.MaterialCategory).ThenInclude(i => i.Category));
            var result = _mapper.Map<IList<StyleDto>>(styles);
            return Ok(result);

        }
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStyle(int id)
        {

            var style = await _unitOfWork.Styles.Get(q => q.Id == id,include: q => q.Include(x => x.Gender).Include(x => x.Products).Include(x => x.MaterialCategory).ThenInclude(i => i.Material).Include(x => x.MaterialCategory).ThenInclude(i => i.Category));
            var result = _mapper.Map<StyleDto>(style);
            return Ok(result);


        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateStyle([FromBody] CreateStyleDto styleDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Post Attempt in { nameof(CreateStyle)}");
                return BadRequest("Invalid Data Inserted");
            }

            var style = _mapper.Map<Style>(styleDTO);
            await _unitOfWork.Styles.Insert(style);
            await _unitOfWork.Save();

            return Created("GetStyle", style);


        }
        [Authorize (Roles = "Administrator")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStyle(int id, [FromBody] UpdateStyleDto StyleDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Update Attempt in { nameof(UpdateStyle)}");
                return BadRequest("Invalid Data Inserted");
            }

            var Style = await _unitOfWork.Styles.Get(q => q.Id == id);
            if (Style == null)
            {
                _logger.LogInformation($"Invalid Update Attempt in { nameof(UpdateStyle)}");
                return BadRequest("Invalid Data Inserted");
            }
            _mapper.Map(StyleDTO, Style);
            _unitOfWork.Styles.Update(Style);
            await _unitOfWork.Save();
            return NoContent();


        }
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteStyle(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteStyle)}");
                return BadRequest();
            }


            var Style = await _unitOfWork.Styles.Get(q => q.Id == id);
            if (Style == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteStyle)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Styles.Delete(id);
            await _unitOfWork.Save();

            return NoContent();

        }

    }
}
