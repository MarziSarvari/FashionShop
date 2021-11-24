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
    public class GenderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GenderController> _logger;
        public GenderController(IUnitOfWork unitOfWork, ILogger<GenderController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGenders()
        {
            var genders = await _unitOfWork.Genders.GetAll(include: q => q.Include(x => x.Styles));
            var result = _mapper.Map<IList<GenderDto>>(genders);
            return Ok(result);



        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGender(int id)
        {

            var gender = await _unitOfWork.Genders.Get(q => q.Id == id, include: q => q.Include(x => x.Styles));
            var result = _mapper.Map<GenderDto>(gender);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGender([FromBody] CreateGenderDto genderDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Post Attempt in { nameof(CreateGender)}");
                return BadRequest("Invalid Data Inserted");
            }

            var gender = _mapper.Map<Gender>(genderDTO);
            await _unitOfWork.Genders.Insert(gender);
            await _unitOfWork.Save();

            return Created("GetGender", gender);


        }
        [Authorize (Roles = "Administrator")]
        [HttpPut("{id:int}" , Name = "UpdateGender")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateGender(int id, [FromBody] UpdateGenderDto genderDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Update Attempt in { nameof(UpdateGender)}");
                return BadRequest("Invalid Data Inserted");
            }

            var gender = await _unitOfWork.Genders.Get(q => q.Id == id);
            if (gender == null)
            {
                _logger.LogInformation($"Invalid Update Attempt in { nameof(UpdateGender)}");
                return BadRequest("Invalid Data Inserted");
            }
            _mapper.Map(genderDTO, gender);
            _unitOfWork.Genders.Update(gender);
            await _unitOfWork.Save();
            return NoContent();


        }
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGender(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteGender)}");
                return BadRequest();
            }


            var gender = await _unitOfWork.Genders.Get(q => q.Id == id);
            if (gender == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteGender)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Genders.Delete(id);
            await _unitOfWork.Save();

            return NoContent();

        }

    }
}
