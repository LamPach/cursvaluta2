using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Cureser
{
    public class ExchangeProvider
    {
        public ExchangeProvider() { }

        public async Task<List<ExchangeModel>> GetExchange()
        {
            List<ExchangeModel> result = new List<ExchangeModel>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage message = await client.GetAsync(@"https://www.cbr-xml-daily.ru/latest.js");
                
                if (message != null)
                {
                    message.EnsureSuccessStatusCode();
                    var body = await message.Content.ReadAsStringAsync();
                    JObject jObject = JObject.Parse(body);
                    Dictionary<string, double> rates = jObject["rates"].ToObject<Dictionary<string, double>>();
                    
                    result = rates.Select(kv => new ExchangeModel() { 
                        Code = kv.Key, 
                        ExchangeRate = kv.Value
                    }).ToList();
                }
            }

            return result;
        }

        public async Task<List<ExchangeModel>> GetExchange(IEnumerable<string> choosen)
        {
            var result = await GetExchange();

            result.RemoveAll((element) => !choosen.Contains(element.Code));

            return result;
        }
    }
}
