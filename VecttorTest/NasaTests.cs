using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using vecttorAPI.Repositories;
using vecttorAPI.Services;

namespace VecttorTest
{
    [TestClass]
    public class NasaTests
    {
        [TestMethod]
        public void CalculaMediaTest()
        {
            var repo = new NasaCalls();
            var resultaDO = repo.CalculaMedia(0.5, 1.5);
            Assert.AreEqual(1, resultaDO);
        }

        [TestMethod]
        public void MeteorosFechaBienTest()
        {

            var repo = new NasaCalls();
            var url = "https://api.nasa.gov/neo/rest/v1/feed?start_date=2015-09-07&end_date=2015-09-09&api_key=BgkFvbvedy9zfdeApHGbbFOMr2cmhOhWMunVhnX2";
            var resultado = repo.GetAsteroides(url).Result;
            Assert.IsNotNull(resultado);
        }
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void MeteorosFechaMalTest()
        {

            var repo = new NasaCalls();
            var url = "https://api.nasa.gov/neo/rest/v1/feed?start_date=2015-09-07&end_date=2015-09-19&api_key=BgkFvbvedy9zfdeApHGbbFOMr2cmhOhWMunVhnX2";
            var resultado = repo.GetAsteroides(url).Result;
            Assert.IsNull(resultado);

        }
        [TestMethod]
        public void GetAllAsteroidesPeligrososTest()
        {
            var repo = new NasaCalls();
            var mirepo = new RepositoryAsteroide(repo);

            var resultado = mirepo.GetUrl("2");
            Assert.IsNotNull(resultado);
        }
        [TestMethod]
        public void GetTop3AsteroidesPeligrososTest()
        {
            var repo = new NasaCalls();
            var mirepo = new RepositoryAsteroide(repo);

            var resultado = mirepo.GetUrl("2");
            Assert.IsNotNull(resultado);
        }
    }
}
