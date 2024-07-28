using HeroTest.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HeroUnitTests
{
    public class TestBase<T>
    {
        protected Mock<SampleContext> _sampleContextMock;

        protected static Mock<DbSet<Brand>> GetQueryableMockSet(IQueryable<T> source)
        {
            var mockSet = new Mock<DbSet<Brand>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(source.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(source.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(source.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(source.GetEnumerator());
            return mockSet;
        }
    }
}