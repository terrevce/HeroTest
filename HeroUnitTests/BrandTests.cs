using HeroTest.Controllers;
using HeroTest.Models;
using HeroTest.Models.RequestModels;
using HeroTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace HeroUnitTests
{
    public class BrandTests : TestBase<Brand>
    {
        private BrandController _brandController;
        private BrandService _brandService;
        private readonly ILogger<BrandController> _brandControllerLogger;
        private readonly ILogger<BrandService> _brandServiceLogger;

        [SetUp]
        public void Setup()
        {
            //Arrange
            var brands = new List<Brand>
            {
                new Brand { Id = 1, Name = "DC", IsActive = true },
                new Brand { Id = 2, Name = "Marvel", IsActive = false },
                new Brand { Id = 3, Name = "Capcom", IsActive = true }
            }.AsQueryable();

            Mock<DbSet<Brand>> mockSet = GetQueryableMockSet(brands); 

            _sampleContextMock = new Mock<SampleContext>();
            _sampleContextMock.Setup(c => c.Brands).Returns(mockSet.Object);

            _brandService = new BrandService(_brandServiceLogger, _sampleContextMock.Object);

            _brandController = new BrandController(_brandControllerLogger, _brandService);

        }

        [Test]
        public void GetOnlyActiveBrands()
        {
            //Act
            var result = _brandController.Get();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

    }
}