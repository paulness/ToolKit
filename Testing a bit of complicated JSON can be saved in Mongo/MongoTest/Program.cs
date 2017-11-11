using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTest
{
    public class OperandsAndOperator
    {
        [JsonProperty("$or")]
        public List<OperandsAndOperator> Or { get; set; }

        [JsonProperty("$and")]
        public List<OperandsAndOperator> And { get; set; }

        [JsonProperty("spr")]
        public int? Professions { get; set; }

        [JsonProperty("spr2")]
        public int? SecondarySpeciality { get; set; }

        [JsonProperty("ssp1")]
        public int? PrimarySpeciality { get; set; }

        [JsonProperty("country_code")]
        public string Country { get; set; }

        [JsonProperty("cc")]
        public string CampaignCode { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            var collection = new MongoClient("").GetServer().GetDatabase("").GetCollection<OperandsAndOperator>("OperandsAndOperator");
            string json = JsonConvert.SerializeObject(new OperandsAndOperator()
            {
                Or = new List<OperandsAndOperator>
                {
                    new OperandsAndOperator() { CampaignCode = "VIP"},
                    new OperandsAndOperator()
                    {
                        And = new List<OperandsAndOperator>
                        {
                            new OperandsAndOperator() { PrimarySpeciality = 77},
                            new OperandsAndOperator() { Country = "UK"}
                        }
                    }
                }
            });
            BsonDocument document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>("{ name : value }");
            QueryDocument queryDoc = new QueryDocument(document);
            MongoCursor toReturn = collection.Find(queryDoc);
        }
    }
}
