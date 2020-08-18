using Api.ModelDTO;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface ICampaignCRUDService
    {
        public CampaignDTO Create(CampaignDTO campaign);
        public bool Update(CampaignDTO campaign);
        public bool Delete(int id);
        public IEnumerable<CampaignDTO> GetAll();
        public CampaignDTO Get(int id);
        public decimal Report();
    }
}
