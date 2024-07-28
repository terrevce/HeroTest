using HeroTest.Models.ResponseModels;
using HeroTest.Models;
using HeroTest.Models.RequestModels;

namespace HeroTest.Services
{
    public class BrandService : IBrandService
    {
        private readonly ILogger<BrandService> _logger;
        private readonly SampleContext _sampleContext;

        public BrandService(ILogger<BrandService> logger, SampleContext sampleContext)
        {
            _logger = logger;
            _sampleContext = sampleContext;
        }

        public IEnumerable<BrandResponse> GetBrands()
        {
            try
            {
                return _sampleContext.Brands.Where(x => x.IsActive == true).Select(i => new BrandResponse
                {
                    Name = i.Name,
                    Id = i.Id
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Enumerable.Empty<BrandResponse>();
            }
        }
        public Brand? GetBrandById(int id)
        {
            try
            {
                return _sampleContext.GetBrandById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public bool CreateBrand(BrandRequest brandRequest, out string message)
        {
            try
            {
                message = string.Empty;

                //check existing
                var brand = _sampleContext.GetBrandByName(brandRequest.Name);
                if (brand != null)
                {
                    message = "Brand Already Exists";
                    return false;
                }

                _sampleContext.Brands.Add(new Brand { Name = brandRequest.Name, IsActive = true });
                _sampleContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public bool DeleteBrand(int id, out string message)
        {
            message = string.Empty;

            try
            {
                //check existing
                var brand = _sampleContext.GetBrandById(id);
                if (brand == null)
                {
                    message = "Active Brand Not Found";
                    return false;
                }

                //set inactive
                brand.IsActive = false;
                _sampleContext.Update(brand);
                _sampleContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message);
                message = ex.Message;
                return false;

            }
        }
    }


}
