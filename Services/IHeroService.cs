using HeroTest.Models.RequestModels;
using HeroTest.Models.ResponseModels;

namespace HeroTest.Services
{
    public interface IHeroService
    {
        public IEnumerable<HeroesResponse> GetHeroes();
        public bool CreateHero(HeroRequest heroRequest, out string message);
        public bool DeleteHero(int Id, out string message);
    }
}