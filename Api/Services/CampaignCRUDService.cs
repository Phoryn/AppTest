using Api.ModelDTO;
using Api.Models;
using Api.Repositories;
using AutoMapper;
using AutoMapper.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class CampaignCRUDService : ICampaignCRUDService
    {
        private readonly IMapper mapper;

        private readonly ICampaignRepository CampaignRepository;
        public CampaignCRUDService(ICampaignRepository campaignRepository,
                                    IMapper mapper)
        {
            CampaignRepository = campaignRepository;
            this.mapper = mapper;
        }

        

        public CampaignDTO Create(CampaignDTO campaign)
        {
            return mapper.Map<CampaignDTO>(CampaignRepository.Create(mapper.Map<Campaign>(campaign)));
        }

        public IEnumerable<CampaignDTO> GetAll()
        {
            return mapper.Map<IEnumerable<CampaignDTO>>(CampaignRepository.GetAll());
        }

        public CampaignDTO Get(int id)
        {
            return mapper.Map<CampaignDTO>(CampaignRepository.Get(id));
        }

        public bool Update(CampaignDTO campaign)
        {
            return CampaignRepository.Update(mapper.Map<Campaign>(campaign));
        }

        public bool Delete(int id)
        {
            return CampaignRepository.Delete(id);
        }

        public decimal Report()
        {
            var collection = mapper.Map<IEnumerable<CampaignDTO>>(CampaignRepository.GetAll());
            decimal temp = 0;
            if (collection != null)
            {
                foreach(var item in collection)
                {
                    temp += item.Cost;
                }
            }
            return temp;
        }
    }
}
