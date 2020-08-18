using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ModelDTO
{
    public class CampaignDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Leader { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}
