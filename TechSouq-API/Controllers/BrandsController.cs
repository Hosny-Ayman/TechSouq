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

        private readonly BrandServices _brandService;

        public BrandsController(BrandServices brandService)
        {
            _brandService = brandService;
        }


        [HttpPost("Create")]
        public async Task <IActionResult> CreateBrand(BrandDto brand)
        {
            var Result = await _brandService.CreateBrand(brand);

            return Ok(Result);
        }

        [HttpGet("Read")]
        public async Task<IActionResult> ReadBrand(int BrandId)
        {
            var Result = await _brandService.ReadBrand(BrandId);

            return Result.Status switch
            {
                OperationStatus.Success => Ok(Result.Data),
                OperationStatus.NotFound => BadRequest(Result.Message),
                _ => StatusCode(500, "Unexpected Error")

            };
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateBrand(BrandDto Brand)
        {
            var Result = await _brandService.UpdateBrand(Brand);

            return Result.Status switch
            {
                OperationStatus.Success => Ok(Result.Message),
                OperationStatus.Failed => BadRequest(Result.Message),
                _ => StatusCode(500, "Unexpected Error")
            };
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteBrand(int BrandId)
        {
            var Result = await _brandService.DeleteBrand(BrandId);

            return Result.Status switch
            {
                OperationStatus.Success => Ok(Result.Message),
                OperationStatus.Failed => BadRequest(Result.Message),
                _ => StatusCode(500, "Unexpected Error")
            };
        }

    }
}
