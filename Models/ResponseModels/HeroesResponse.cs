namespace HeroTest.Models.ResponseModels
{
    public class HeroesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Alias { get; set; } = null!;
        public string BrandName { get; set; } = null!;
    }
}
