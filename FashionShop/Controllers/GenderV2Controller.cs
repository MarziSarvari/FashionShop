using AutoMapper;
using FashionShop.Data;
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
    [ApiVersion("2.0")]
    [Route("api/gender")]
    [ApiController]
    public class GenderV2Controller : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<GenderController> _logger;
        public GenderV2Controller(AppDbContext appDbContext, ILogger<GenderController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGenders()
        {
            return Ok(_appDbContext.Genders);

        }
        
        

    }
}
