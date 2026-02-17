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
    public class BrandServices
    {
        private readonly IBrandRepository _brandRepository;

        public BrandServices(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<int> CreateBrand(BrandDto brand)
        {
            Brand br = new Brand();

            br.Name = brand.Name;

           return await _brandRepository.CreateBrand(br);
        }

        public async Task<OperationResult <bool>> DeleteBrand(int brandId)
        {
            if(brandId < 0)
            {
                return new OperationResult<bool>
                {
                    Message = "Id Most be Postitave",
                    Status = OperationStatus.NotFound
                };

            }
            else
            {

                var Result = await _brandRepository.DeleteBrand(brandId);

                if(Result)
                {
                    return new OperationResult<bool>
                    {
                        Message = "Deleted Successfully",
                        Status = OperationStatus.Success
                    };
                }
                else
                {
                    return new OperationResult<bool>
                    {
                        Message = "Deleted Failed",
                        Status = OperationStatus.Failed
                    };
                }

               
            }
        }

        public async Task<OperationResult<BrandDto>> ReadBrand(int BrandId)
        {

            if(BrandId < 0)
            {
                return new OperationResult<BrandDto>
                {
                    Message = "Id Most be Postitave",
                    Status = OperationStatus.NotFound,
                    Data = null
                };
            }

            else
            {
                Brand Result = await _brandRepository.ReadBrand(BrandId);



                if(Result!=null)
                {
                    return new OperationResult<BrandDto>
                    {

                        Status = OperationStatus.Success,
                        Data = new BrandDto
                        {
                            Id = Result.Id,
                            Name = Result.Name,
                        }
                       
                    };
                }
                else
                {
                    return new OperationResult<BrandDto>
                    {
                        Message = "Deleted Failed",
                        Status = OperationStatus.Failed,
                        Data = null
                    };
                }

            }

        }

        public async Task<OperationResult<bool>> UpdateBrand(BrandDto brand)
        {

            Brand br = new Brand();

            br.Id = brand.Id;
            br.Name = brand.Name;

            var Result = await _brandRepository.UpdateBrand(br);

            if(Result)
            {
                return new OperationResult<bool>
                {
                    Status = OperationStatus.Success,
                    Message = "Brand Update Successfully"
                };
            }
            else
            {
                return new OperationResult<bool>
                {
                    Status = OperationStatus.Failed,
                    Message = "Brand Update Failed"
                };
            }
        }


    }
}
