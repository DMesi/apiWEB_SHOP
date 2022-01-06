using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using webWS.Models;

namespace webWS.Repository
{
    public class LocationRepository : Repository<Location>,ILocationRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public LocationRepository(IHttpClientFactory clientFactory):base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
