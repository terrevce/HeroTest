using System.ComponentModel.DataAnnotations;

namespace HeroTest.Models.RequestModels
{
    public class HeroRequest
    {
        [MinLength(1, ErrorMessage = "Name cannot be empty.")]
        public string Name { get; set; } = null!;
        public string? Alias { get; set; }
        public string BrandName { get; set; } = null!;

    }
}
