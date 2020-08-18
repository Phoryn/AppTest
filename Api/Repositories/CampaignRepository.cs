using Dapper;
using Dapper.Contrib.Extensions;
using Api.Infrastructure;
using Api.Models;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Api.Repositories
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly string connStr;
        public CampaignRepository(IOptions<ConnStr> connectionStrings)
        {
            connStr = connectionStrings.Value.connStr;
        }
        public Campaign Create(Campaign campaign)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                campaign.Id = 1;
                 //var z = connection.Execute("INSERT INTO Kampania (Name, Description, Leader, Cost) VALUES('" + campaign.Name.ToString() + "', '" + campaign.Description.ToString() + "', '" + campaign.Leader.ToString() + "', " + campaign.Cost.ToString() + ")");
                 var id = connection.Insert(campaign);
                return connection.Get<Campaign>(id);
            }
        }

        public Campaign Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                return connection.Get<Campaign>(id);
            }
        }

        public IEnumerable<Campaign> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                return connection.GetAll<Campaign>();
            }
        }

        public bool Update(Campaign campaign)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                return connection.Update(campaign);
            }
        }
        public bool Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                return connection.Delete(new Campaign { Id = id });
            }
        }


    }
}
