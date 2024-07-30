using System.Security;
using System.ComponentModel.DataAnnotations;

namespace HeroTest.Models.RequestModels
{
    public class BrandRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "The Name field must be a string with a maximum length of 100.")]
        public String Name { get; set; } = null!;
    }
}
