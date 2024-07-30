using HeroTest.Controllers;
using HeroTest.Models;
using HeroTest.Models.RequestModels;
using HeroTest.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace HeroTest.Services
{
    public class HeroService : IHeroService
    {
        private readonly SampleContext _sampleContext;
        private readonly ILogger<HeroService> _logger;
        public HeroService(ILogger<HeroService> logger, SampleContext sampleContext)
        {
            _logger = logger;
            _sampleContext = sampleContext;
        }


        public async Task<IEnumerable<HeroesResponse>> GetHeroesAsync()
        {
            return await _sampleContext.Heroes.Where(x => x.IsActive == true).Select(i => new HeroesResponse
            {
                Alias = i.Alias ?? string.Empty,
                BrandName = i.Brand.Name,
                Id = i.Id,
                Name = i.Name
            }).ToListAsync();
        }

        public async Task<ServiceResult> CreateHeroAsync(HeroRequest heroRequest)
        {
            try
            {
                Brand? brand = await _sampleContext.GetBrandByNameAsync(heroRequest.BrandName);
                if (brand == null)
                {                    
                    return new ServiceResult(false, "Brand Not Found");
                }

                await _sampleContext.Heroes.AddAsync(new Hero { Alias = heroRequest.Alias, Name = heroRequest.Name, IsActive = true, BrandId = brand.Id });
                await _sampleContext.SaveChangesAsync();
                return new ServiceResult(true, string.Empty);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message);                
                return new ServiceResult(false,ex.Message);
            }
        }

        public async Task<ServiceResult> DeleteHeroAsync(int Id)
        {
            
            try
            {
                Hero? hero = await _sampleContext.Heroes.Where(x => x.Id == Id && x.IsActive == true).FirstOrDefaultAsync();

                if (hero == null)
                {
                    return new ServiceResult(false, "Hero Not Found");                                        
                }

                hero.IsActive = false;
                _sampleContext.Update(hero);
                await _sampleContext.SaveChangesAsync();
                return new ServiceResult(true,string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message);                
                return new ServiceResult(false,ex.Message);
            }          

        }
    }
}
