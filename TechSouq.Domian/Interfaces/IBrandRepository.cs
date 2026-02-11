using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSouq.Domain.Entities;

namespace TechSouq.Domian.Interfaces
{
    public interface IBrandRepository
    {

        Task <int> CreateBrand (Brand brand);

        Task<Brand> ReadBrand(int BrandId);

        Task <bool> UpdateBrand(Brand brand);

        Task <bool> DeleteBrand (int brandId);

       

    }
}
