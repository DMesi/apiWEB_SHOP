using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using webWS.Models;
using webWS.Repository.IRepository;

namespace webWS.Repository
{
    public class AccountRepository : Repository<Users>, IAccountRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public AccountRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Users> LoginAsync(string url, Users objecToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
           
            if (objecToCreate != null)
            {
                 request.Content = new StringContent(JsonConvert.SerializeObject(objecToCreate), Encoding.UTF8, "application/json"); // ovo
            }
            else
            { 
                return new Users(); 
            }

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
           // if (response.StatusCode == System.Net.HttpStatusCode.OK)
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {

                var jsonString = await response.Content.ReadAsStringAsync();


                return JsonConvert.DeserializeObject<Users>(jsonString);
            }
            else
            {
                return new Users();
            }
        }

        public async Task<bool> RegisterAsync(string url, Users objecToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            if (objecToCreate != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objecToCreate), Encoding.UTF8, "application/json");
            }
            else
            { return false; }

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            // if (response.StatusCode == System.Net.HttpStatusCode.OK)
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
