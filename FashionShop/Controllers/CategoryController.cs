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
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(IUnitOfWork unitOfWork, ILogger<CategoryController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategories()
        {
          
            var categories = await _unitOfWork.Categories.GetAll(include: q => q.Include(x => x.MaterialCategories).ThenInclude(i => i.Material));
            var result = _mapper.Map<IList<CategoryDto>>(categories);
            return Ok(result);

        }
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategory(int id)
        {

            var category = await _unitOfWork.Categories.Get(q => q.Id == id, include: q => q.Include(x => x.MaterialCategories).ThenInclude(i => i.Material));
            var result = _mapper.Map<CategoryDto>(category);
            return Ok(result);


        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Post Attempt in { nameof(CreateCategory)}");
                return BadRequest("Invalid Data Inserted");
            }

            var category = _mapper.Map<Category>(categoryDTO);
            await _unitOfWork.Categories.Insert(category);
            await _unitOfWork.Save();

            return Created("GetCategory", category);


        }
        [Authorize (Roles = "Administrator")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Update Attempt in { nameof(UpdateCategory)}");
                return BadRequest("Invalid Data Inserted");
            }

            var category = await _unitOfWork.Categories.Get(q => q.Id == id);
            if (category == null)
            {
                _logger.LogInformation($"Invalid Update Attempt in { nameof(UpdateCategory)}");
                return BadRequest("Invalid Data Inserted");
            }
            _mapper.Map(categoryDTO, category);
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.Save();
            return NoContent();


        }
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCategory)}");
                return BadRequest();
            }


            var category = await _unitOfWork.Categories.Get(q => q.Id == id);
            if (category == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCategory)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Categories.Delete(id);
            await _unitOfWork.Save();

            return NoContent();

        }

    }
}
