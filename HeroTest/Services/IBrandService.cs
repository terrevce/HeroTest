using HeroTest.Models;
using HeroTest.Models.RequestModels;
using HeroTest.Models.ResponseModels;

namespace HeroTest.Services
{
    public interface IBrandService
    {
        public Task<ServiceResult> CreateBrandAsync(BrandRequest brandRequest);
        public Task<ServiceResult> DeleteBrandAsync(int id);
        public Task<Brand?> GetBrandByIdAsync(int id);
        public Task<IEnumerable<BrandResponse>> GetBrandsAsync();
    }
}