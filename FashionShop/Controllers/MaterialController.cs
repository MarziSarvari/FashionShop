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
    public class MaterialController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<MaterialController> _logger;
        public MaterialController(IUnitOfWork unitOfWork, ILogger<MaterialController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMaterials([FromQuery] RequestParams requestParams)
        {
          
            var materials = await _unitOfWork.Materials.GetAll(requestParams, include: q => q.Include(x => x.MaterialCategories).ThenInclude(i => i.Category));
            var result = _mapper.Map<IList<MaterialDto>>(materials);
            return Ok(result);

        }
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMaterial(int id)
        {

            var material = await _unitOfWork.Materials.Get(q => q.Id == id, include: q => q.Include(x => x.MaterialCategories).ThenInclude(i => i.Category));
            var result = _mapper.Map<MaterialDto>(material);
            return Ok(result);


        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMaterial([FromBody] CreateMaterialDto materialDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Post Attempt in { nameof(CreateMaterial)}");
                return BadRequest("Invalid Data Inserted");
            }

            var material = _mapper.Map<Material>(materialDTO);
            await _unitOfWork.Materials.Insert(material);
            await _unitOfWork.Save();

            return Created("GetMaterial", material);


        }
        [Authorize (Roles = "Administrator")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMaterial(int id, [FromBody] UpdateMaterialDto materialDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Update Attempt in { nameof(UpdateMaterial)}");
                return BadRequest("Invalid Data Inserted");
            }

            var material = await _unitOfWork.Materials.Get(q => q.Id == id);
            if (material == null)
            {
                _logger.LogInformation($"Invalid Update Attempt in { nameof(UpdateMaterial)}");
                return BadRequest("Invalid Data Inserted");
            }
            _mapper.Map(materialDTO, material);
            _unitOfWork.Materials.Update(material);
            await _unitOfWork.Save();
            return NoContent();


        }
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteMaterial)}");
                return BadRequest();
            }


            var material = await _unitOfWork.Materials.Get(q => q.Id == id);
            if (material == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteMaterial)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Materials.Delete(id);
            await _unitOfWork.Save();

            return NoContent();

        }

    }
}
