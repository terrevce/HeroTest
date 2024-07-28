using HeroTest.Controllers;
using HeroTest.Models;
using HeroTest.Models.RequestModels;
using HeroTest.Models.ResponseModels;

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


        public IEnumerable<HeroesResponse> GetHeroes()
        {
            return _sampleContext.Heroes.Where(x => x.IsActive == true).Select(i => new HeroesResponse
            {
                Alias = i.Alias,
                BrandName = i.Brand.Name,
                Id = i.Id,
                Name = i.Name
            }).ToList();
        }

        public bool CreateHero(HeroRequest heroRequest, out string message)
        {
            try
            {
                message = string.Empty;

                Brand? brand = _sampleContext.GetBrandByName(heroRequest.BrandName);
                if (brand == null)
                {
                    message = "Brand Not Found";
                    return false;
                }

                _sampleContext.Heroes.Add(new Hero { Alias = heroRequest.Alias, Name = heroRequest.Name, IsActive = true, BrandId = brand.Id });
                _sampleContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message);
                message = "Inernal Error";
                return false;
            }
        }

        public bool DeleteHero(int Id,out string message)
        {
            message= string.Empty;
            try
            {
                var hero = _sampleContext.Heroes.Where(x => x.Id == Id && x.IsActive == true).FirstOrDefault();

                if (hero == null)
                {
                    message = "Hero Not Found";
                    return false;
                }

                hero.IsActive = false;
                _sampleContext.Update(hero);
                _sampleContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message);
                message = "Internal Error";
                return false;
            }          

        }
    }
}
