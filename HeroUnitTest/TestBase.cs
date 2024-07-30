using HeroTest.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HeroUnitTests
{
    public class TestBase
    {
        protected Mock<SampleContext> _sampleContextMock;

        protected static Mock<DbSet<T>> GetQueryableMockSet<T>(IQueryable<T> source) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(source.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(source.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(source.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(source.GetEnumerator());
            return mockSet;
        }


    }
}