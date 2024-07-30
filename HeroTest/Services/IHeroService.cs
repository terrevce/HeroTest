using HeroTest.Models.RequestModels;
using HeroTest.Models.ResponseModels;

namespace HeroTest.Services
{
    public interface IHeroService
    {
        public Task<IEnumerable<HeroesResponse>> GetHeroesAsync();
        public Task<ServiceResult> CreateHeroAsync(HeroRequest heroRequest);
        public Task<ServiceResult> DeleteHeroAsync(int Id);
    }
}