using AutoMapper;
using IdentityServer.DTOs;
using IdentityServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Mapper
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<User, NewUserDto>().ReverseMap();
            CreateMap<User, UserDetails>().ReverseMap();
        }
    }
}
