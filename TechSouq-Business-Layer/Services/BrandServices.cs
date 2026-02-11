using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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




    }
}
