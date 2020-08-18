using Api.ModelDTO;
using Api.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Infrastructure
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Campaign, CampaignDTO>().ReverseMap(); // means you want to map from User to UserDTO
        }
    }
}
