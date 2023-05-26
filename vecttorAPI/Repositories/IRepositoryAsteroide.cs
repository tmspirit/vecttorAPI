using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vecttorAPI.Models;

namespace vecttorAPI.Repositories
{
    public interface IRepositoryAsteroide
    {
        List<Asteroide> AsteropidesPeligrosos(string days);
        List<Asteroide> Top3AsteropidesPeligrosos(string days);
        String GetUrl(string days);
    }
}
