using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSouq.Application.Dtos;
using TechSouq.Domain.Entities;

namespace TechSouq.Application.Mappings
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles ()
        {
            CreateMap<CartItemDto, CartItem>().ReverseMap();
        }

    }
}
