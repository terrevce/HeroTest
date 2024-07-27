namespace HeroTest.Models.ResponseModels
{
    public class HeroesResponse: IResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Alias { get; set; } = null!;
        public string BrandName { get; set; } = null!;
    }
}
