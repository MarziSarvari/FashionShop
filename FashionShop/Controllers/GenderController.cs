using AutoMapper;
using FashionShop.Models.Dtos;
using FashionShop.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            throw new Exception();

            var genders = await _unitOfWork.Genders.GetAll(includes: new List<string> { "Styles" });
            var result = _mapper.Map<IList<GenderDto>>(genders);
            return Ok(result);



        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGender(int id)
        {

            var gender = await _unitOfWork.Genders.Get(q => q.Id == id, new List<string> { "Style" });
            var result = _mapper.Map<GenderDto>(gender);
            return Ok(result);
        }

    }
}
