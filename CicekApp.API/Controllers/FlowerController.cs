using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Services.FlowerService;
using CicekApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CicekApp.API.Controllers
{
    [ApiController]
    [Route("api/flowers")]
    public class FlowerController : ControllerBase
    {
        private readonly IFlowerService _flowerService;

        public FlowerController(IFlowerService flowerService)
        {
            _flowerService = flowerService;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<Flower>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var flowers = await _flowerService.GetAllAsync();
            return Ok(flowers);
        }


        [HttpGet]
        [Route("/{id}")]
        [ProducesResponseType(typeof(List<Flower>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var flower = await _flowerService.GetByIdAsync(id);
            return Ok(flower);
        }

    }
}