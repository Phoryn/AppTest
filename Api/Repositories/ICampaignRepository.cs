using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface ICampaignRepository
    {
        public Campaign Create(Campaign campaign);
        public bool Update(Campaign id);
        public IEnumerable<Campaign> GetAll();
        public Campaign Get(int id);
        public bool Delete(int id);
    }
}
