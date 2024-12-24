using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Services.CategoryService;
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
        private readonly ICategoryService _categoryService;

        public FlowerController(IFlowerService flowerService, ICategoryService categoryService)
        {
            _flowerService = flowerService;
            _categoryService = categoryService;
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
        [Route("{id}")]
        [ProducesResponseType(typeof(List<Flower>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var flower = await _flowerService.GetByIdAsync(id);
            return Ok(flower);
        }

        [HttpGet]
        [Route("categories")]
        [ProducesResponseType(typeof(List<Category>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories()
        {
            var flowers = await _categoryService.GetAllAsync();
            return Ok(flowers);
        }


    }
}