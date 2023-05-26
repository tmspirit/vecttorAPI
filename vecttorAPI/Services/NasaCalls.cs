using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using vecttorAPI.Models;

namespace vecttorAPI.Services
{
    public class NasaCalls : INasaCalls
    {

        public async Task<List<Asteroide>> GetAsteroides(string url)
        {
            List<NearEarthObjects> nearEarthObjectsLista = new List<NearEarthObjects>();
            List<Asteroide> asteroidesPeligrosos = new List<Asteroide>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    nearEarthObjectsLista = await ListaAsteroidesAsync(response);

                    //var consulta = (from datos in nearEarthObjectsLista orderby datos.mediaDiametro descending select datos).Take(3);


                    foreach (var item in nearEarthObjectsLista)
                    {
                        Asteroide miAsteroide = new Asteroide();
                        miAsteroide.nombre = item.name;
                        miAsteroide.diametro = item.mediaDiametro;
                        miAsteroide.velocidad = item.close_approach_data.relative_velocity.kilometers_per_hour;
                        miAsteroide.fecha = item.close_approach_data.close_approach_date;
                        miAsteroide.planeta = item.close_approach_data.orbiting_body;

                        asteroidesPeligrosos.Add(miAsteroide);

                    }
                }
                else
                {
                    var res = response.Content.ReadAsStringAsync().GetAwaiter().GetResult().Contains("The Feed date limit is only 7 Days");
                    if (res)
                    {
                        throw new Exception("Date Format Exception - Expected format (yyyy-mm-dd) - The Feed date limit is only 7 Days");
                    }
                    else
                    {
                        throw new Exception(HttpStatusCode.BadRequest.ToString());
                    }


                }
            }

            return asteroidesPeligrosos;
        }

        public async Task<List<NearEarthObjects>> ListaAsteroidesAsync(HttpResponseMessage response)
        {

            List<NearEarthObjects> nearEarthObjectsLista = new List<NearEarthObjects>();
            var resJSON = await response.Content.ReadAsStringAsync();

            JObject parent = JObject.Parse(resJSON);
            var dias = parent.Value<JObject>("near_earth_objects").Properties().ToList();

            foreach (var item in dias)
            {
                foreach (var item2 in item.Value)
                {
                    var obj = item2.ToList();
                    var is_potentially_hazardous_asteroid = bool.Parse(obj[7].First.ToString());

                    if (is_potentially_hazardous_asteroid)
                    {
                        NearEarthObjects nearEarthObjects = new NearEarthObjects();
                        nearEarthObjects.name = obj[3].First.ToString();
                        nearEarthObjects.estimated_diameter = obj[6].First.ToObject<EstimatedDiameter>();
                        nearEarthObjects.mediaDiametro = CalculaMedia(nearEarthObjects.estimated_diameter.kilometers.estimated_diameter_max, nearEarthObjects.estimated_diameter.kilometers.estimated_diameter_min);
                        nearEarthObjects.close_approach_data = obj[8].First.ToObject<List<CloseApproachDatum>>().FirstOrDefault();

                        nearEarthObjectsLista.Add(nearEarthObjects);
                    }

                }
            }

            return nearEarthObjectsLista;

        }

        public double CalculaMedia(double a, double b)
        {
            return ((a + b) / 2);
        }

    }
}
