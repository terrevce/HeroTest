using HeroTest.Models;
using HeroTest.Models.RequestModels;
using HeroTest.Models.ResponseModels;

namespace HeroTest.Services
{
    public interface IBrandService
    {
        public bool CreateBrand(BrandRequest brandRequest, out string message);
        public bool DeleteBrand(int id, out string message);
        public Brand? GetBrandById(int id);
        public IEnumerable<BrandResponse> GetBrands();
    }
}