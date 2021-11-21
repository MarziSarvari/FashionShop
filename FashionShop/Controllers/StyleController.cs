using AutoMapper;
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
        public async Task<IActionResult> GetStyles()
        {
          
            var styles = await _unitOfWork.Styles.GetAll(include: q => q.Include(x => x.Gender).Include(x => x.Products).Include(x => x.MaterialCategory).ThenInclude(i => i.Material).Include(x => x.MaterialCategory).ThenInclude(i => i.Category));
            var result = _mapper.Map<IList<StyleDto>>(styles);
            return Ok(result);

        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStyle(int id)
        {

            var style = await _unitOfWork.Styles.Get(q => q.Id == id,include: q => q.Include(x => x.Gender).Include(x => x.Products).Include(x => x.MaterialCategory).ThenInclude(i => i.Material).Include(x => x.MaterialCategory).ThenInclude(i => i.Category));
            var result = _mapper.Map<StyleDto>(style);
            return Ok(result);


        }

    }
}
