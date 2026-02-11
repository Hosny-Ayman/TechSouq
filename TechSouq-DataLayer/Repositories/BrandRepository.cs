using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSouq.Domain.Entities;
using TechSouq.Domian.Interfaces;
using TechSouq.Infrastructure.Data;

namespace TechSouq.DataLayer.Repositories
{
    public class BrandRepository : IBrandRepository
    {

        private readonly AppDbContext _AppDbContext;

        public BrandRepository (AppDbContext AppDbContext)
        {
            _AppDbContext = AppDbContext;
        }

        public async Task<int> CreateBrand(Brand brand)
        {
            await _AppDbContext.Brands.AddAsync(brand);

            await _AppDbContext.SaveChangesAsync();

            return brand.Id;
        }

        public async Task<bool> DeleteBrand(int brandId)
        {
           return await _AppDbContext.Brands.Where(x => x.Id == brandId).ExecuteDeleteAsync() > 0;

        }

        public async Task<Brand> ReadBrand(int BrandId)
        {
            return await _AppDbContext.Brands.AsNoTracking().FirstOrDefaultAsync(x => x.Id == BrandId);
        }

        public async Task<bool> UpdateBrand(Brand brand)
        {
            _AppDbContext.Brands.Update(brand);

            return await _AppDbContext.SaveChangesAsync() > 0;
        }
    }
}
