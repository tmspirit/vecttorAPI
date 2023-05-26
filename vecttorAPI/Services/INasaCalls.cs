using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using vecttorAPI.Models;

namespace vecttorAPI.Services
{
    public interface INasaCalls
    {
        Task<List<Asteroide>> GetAsteroides(string url);
        Task<List<NearEarthObjects>> ListaAsteroidesAsync(HttpResponseMessage response);
    }
}
