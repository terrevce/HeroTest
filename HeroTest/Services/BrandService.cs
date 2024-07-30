using HeroTest.Models.ResponseModels;
using HeroTest.Models;
using HeroTest.Models.RequestModels;
using Microsoft.EntityFrameworkCore;
using System;

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

        public async Task<IEnumerable<BrandResponse>> GetBrandsAsync()
        {
            try
            {
                return await _sampleContext.Brands.Where(x => x.IsActive == true).Select(i => new BrandResponse
                {
                    Name = i.Name,
                    Id = i.Id
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Enumerable.Empty<BrandResponse>();
            }
        }
        public async Task<Brand?> GetBrandByIdAsync(int id)
        {
            try
            {
                return await _sampleContext.GetBrandByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<ServiceResult> CreateBrandAsync(BrandRequest brandRequest)
        {            
            try
            {               

                //check existing
                var brand = await _sampleContext.GetBrandByNameAsync(brandRequest.Name);
                if (brand != null)
                {                    
                    return new ServiceResult(false, "Brand Already Exists");
                }

                await _sampleContext.Brands.AddAsync(new Brand { Name = brandRequest.Name, IsActive = true });
                await _sampleContext.SaveChangesAsync();
                return new ServiceResult(true, string.Empty);
            }
            catch (Exception ex)
            {                
                _logger.LogError(ex, ex.Message);
                return new ServiceResult(false,ex.Message);
            }
        }

        public async Task<ServiceResult> DeleteBrandAsync(int id)
        { 
            try
            {
                //check existing
                var brand = await _sampleContext.GetBrandByIdAsync(id);
                if (brand == null)
                {                    
                    return new ServiceResult(false, "Active Brand Not Found");
                }

                //set inactive
                brand.IsActive = false;
                _sampleContext.Update(brand);
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
