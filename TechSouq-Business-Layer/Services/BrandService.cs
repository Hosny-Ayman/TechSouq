using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSouq.Application.Dtos;
using TechSouq.Domain.Entities;
using TechSouq.Domian.Interfaces;

namespace TechSouq.Application.Services
{
    public class BrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BrandService> _logger;

        public BrandService(IBrandRepository brandRepository, IMapper mapper,ILogger<BrandService> logger)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult<int>> AddBrand(BrandDto brandDto)
        {
           

            try
            {
                var brand = _mapper.Map<Brand>(brandDto);
                var newId =  await _brandRepository.AddBrand(brand);

                _logger.LogInformation("Brand Created With Id : {Id} Successfuly", newId);
                return OperationResult<int>.Success(newId);
            }

            catch(Exception ex)
            {
                _logger.LogError(ex,"Brand Created With Failed");
                return OperationResult<int>.Failure("Create Brand Failed Try Later");
            }
        }


        public async Task<OperationResult<bool>> DeleteBrand(int brandId)
        {
            if(brandId <= 0)
            {
                _logger.LogWarning("Delete Brand With Id : {brandId} Invalid", brandId);
                return OperationResult<bool>.BadRequest("Invalid Data", new List<string> { $"Invalid brandId {brandId}" });
            }
            try
            {
                var result = await _brandRepository.DeleteBrand(brandId);

                _logger.LogInformation("Delete Brand With Id : {brandId} Successfully", brandId);
                return OperationResult<bool>.Success(result);
            }

            catch(Exception ex)
            {
                _logger.LogError(ex,"Delete Brand With Id : {brandId} Failed", brandId);
                return OperationResult<bool>.Failure("Delete Brand Failed Try Later");
            }
          
        }



        public async Task<OperationResult<BrandDto>> GetBrand(int brandId)
        {

            if(brandId <= 0)
            {
                _logger.LogWarning("Read Brand With Id : {brandId} Invalid", brandId);
                return OperationResult<BrandDto>.BadRequest("Invalid Data", new List<string> { $"Invalid brandId {brandId}" });
            }

            try
            {
                var result = await _brandRepository.GetBrand(brandId);

                if(result == null)
                {
                    _logger.LogWarning("Read Brand With Id : {brandId} NotFound", brandId);
                    return OperationResult<BrandDto>.NotFound($"Brand With Id NotFound {brandId}");
                }

                var brandDto = _mapper.Map<BrandDto>(result);

                _logger.LogInformation("Read Brand With Id : {brandId}", brandId);
                return OperationResult<BrandDto>.Success(brandDto);
            }

            catch(Exception ex)
            {
                _logger.LogError(ex,"Read Brand With Id : {brandId} Failed", brandId);
                return OperationResult<BrandDto>.Failure("Read Brand Failed Try Later");
            }
           
        }

        public async Task<OperationResult<bool>> UpdateBrand(BrandDto brandDto)
        {

            var brand = _mapper.Map<Brand>(brandDto);

            try
            {
                var result = await _brandRepository.UpdateBrand(brand);

               if(!result)
                {
                    _logger.LogError("Update Brand Failed brand Data {@brand} ", brand);
                    return OperationResult<bool>.Failure("Update Brand Filaed Try Later");
                }

                _logger.LogInformation("Update Brand With Id: {Id} Successfully", brand.Id);
                return OperationResult<bool>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Update Brand Failed brand Data {brand} ", brand);
                return OperationResult<bool>.Failure("Update Brand Filaed Try Later");
            }
           
        }

    }
}
