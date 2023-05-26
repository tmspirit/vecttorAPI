using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vecttorAPI.Models;
using vecttorAPI.Services;

namespace vecttorAPI.Repositories
{
    public class RepositoryAsteroide : IRepositoryAsteroide
    {
        public INasaCalls repo;
        public RepositoryAsteroide(INasaCalls repo)
        {
            this.repo = repo;
        }
        public List<Asteroide> AsteropidesPeligrosos(string days)
        {
            List<Asteroide> asteroidesPeligrosos = new List<Asteroide>();
            string url = GetUrl(days);

            asteroidesPeligrosos = repo.GetAsteroides(url).Result;

            return asteroidesPeligrosos;
        }
        public List<Asteroide> Top3AsteropidesPeligrosos(string days)
        {
            List<Asteroide> asteroidesPeligrosos = new List<Asteroide>();

            string url = GetUrl(days);
            asteroidesPeligrosos = repo.GetAsteroides(url).Result;

            var top3 = (from datos in asteroidesPeligrosos orderby datos.diametro descending select datos).Take(3).ToList();

            return top3;
        }
        public String GetUrl(string days)
        {
            string url = "https://api.nasa.gov/neo/rest/v1/feed?start_date=";
            string hoy = DateTime.Today.ToString("yyyy-MM-dd");
            string fechaLimite = DateTime.Today.AddDays(Double.Parse(days)).ToString("yyyy-MM-dd");

            url = url + hoy + "&end_date=" + fechaLimite + "&api_key=BgkFvbvedy9zfdeApHGbbFOMr2cmhOhWMunVhnX2";
            return url;
        }

    }
}
