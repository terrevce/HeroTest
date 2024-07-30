using HeroTest.Controllers;
using HeroTest.Models;
using HeroTest.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroUnitTests
{
    internal class HeroTests : TestBase
    {

        private HeroesController _heroController;
        private HeroService _heroService;
        private readonly ILogger<HeroesController> _heroControllerLogger;
        private readonly ILogger<HeroService> _heroServiceLogger;

        [SetUp]
        public void Setup()
        {
            //Arrange
            var brands = new List<Brand>
            {
                new Brand { Id = 1, Name = "DC", IsActive = true }
            }.AsQueryable();

            var heroes = new List<Hero>
            {
                new Hero { Id = 1, Name = "SuperMan", IsActive = true, BrandId=1, Brand = brands.First(b => b.Id == 1) },
                new Hero { Id = 2, Name = "BatMan", IsActive = false, BrandId=1, Brand = brands.First(b => b.Id == 1)  },
                new Hero { Id = 3, Name = "Spidey", IsActive = true, BrandId=1 , Brand = brands.First(b => b.Id == 1)}
            }.AsQueryable();

            Mock<DbSet<Hero>> mockHeroSet = GetQueryableMockSet(heroes);
            Mock<DbSet<Brand>> mocBrandSet = GetQueryableMockSet(brands);

            _sampleContextMock = new Mock<SampleContext>();
            _sampleContextMock.Setup(c => c.Heroes).ReturnsDbSet(mockHeroSet.Object);
            _sampleContextMock.Setup(c => c.Brands).ReturnsDbSet(mocBrandSet.Object);

            _heroService = new HeroService(_heroServiceLogger, _sampleContextMock.Object);

            _heroController = new HeroesController(_heroControllerLogger, _heroService);

        }

        [Test]
        public async Task GetOnlyActiveHeroes()
        {
            //Act
            var result = await _heroController.Get();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
