using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TechSouq.Application;
using TechSouq.Application.Dtos;
using TechSouq.Application.Services;

namespace TechSouq.API.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ApiController]
    public class BrandsController : ControllerBase
    {

        private readonly BrandService _brandService;

        public BrandsController(BrandService brandService)
        {
            _brandService = brandService;
        }


        [HttpPost("Create")]
        public async Task <IActionResult> CreateBrand(BrandDto brand)
        {
            var result = await _brandService.CreateBrand(brand);

            return result.Status switch
            {
                OperationStatus.Success => Ok(result),
                OperationStatus.NotFound => NotFound(result),
                OperationStatus.BadRequest => BadRequest(result),
                _ => StatusCode(500, result)

            };
        }

        [HttpGet("Read")]
        public async Task<IActionResult> ReadBrand(int BrandId)
        {
            var result = await _brandService.ReadBrand(BrandId);

            return result.Status switch
            {
                OperationStatus.Success => Ok(result),
                OperationStatus.NotFound => NotFound(result),
                OperationStatus.BadRequest => BadRequest(result),
                _ => StatusCode(500, result)

            };
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateBrand(BrandDto Brand)
        {
            var result = await _brandService.UpdateBrand(Brand);

            return result.Status switch
            {
                OperationStatus.Success => Ok(result),
                OperationStatus.NotFound => NotFound(result),
                OperationStatus.BadRequest => BadRequest(result),
                _ => StatusCode(500, result)
            };
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteBrand(int BrandId)
        {
            var result = await _brandService.DeleteBrand(BrandId);

            return result.Status switch
            {
                OperationStatus.Success => Ok(result),
                OperationStatus.NotFound => NotFound(result),
                OperationStatus.BadRequest => BadRequest(result),
                _ => StatusCode(500, result)
            };
        }

    }
}
