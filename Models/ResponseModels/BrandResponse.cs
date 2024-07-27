namespace HeroTest.Models.ResponseModels
{
    public class BrandResponse : IResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}