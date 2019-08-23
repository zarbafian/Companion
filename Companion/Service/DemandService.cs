using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

using Companion.Models;

namespace Companion.Service
{
    public class DemandService
    {
        HttpClient _client;

        public DemandService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("sid", Constants.ApiToken);
        }

        public async Task<UserNotification> CreateDemandDataAsync(Demand demand)
        {
            UserNotification userNotification = new UserNotification{
                Title=demand.Title,
                Content=demand.Content
            };

            try
            {
                string uri = Constants.BackboneEndpoint + "/cmc/notifications";
                string body = JsonConvert.SerializeObject(userNotification);
                
                var stringContent = new StringContent(body, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(uri, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    string reponseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UserNotification>(reponseBody);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
                return null;
            }
        }
    }
}
