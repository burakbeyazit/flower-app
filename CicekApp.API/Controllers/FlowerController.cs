using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Services.FlowerService;
using Microsoft.AspNetCore.Mvc;

namespace CicekApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlowerController : ControllerBase
    {
        private readonly IFlowerService _flowerService;

        public FlowerController(IFlowerService flowerService)
        {
            _flowerService = flowerService;
        }

        // [HttpGet]
        // [Route("")]
        // public async Task<IActionResult> GetAll()
        // {
        //     var flowers = _flowerService.
        // }
    }
}