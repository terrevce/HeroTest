using HeroTest.Models.RequestModels;
using HeroTest.Models.ResponseModels;
using HeroTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeroTest.Controllers;
[ApiController]
[Route("[controller]")]
public class BrandController : ControllerBase
{

    private readonly ILogger<BrandController> _logger;
    private readonly IBrandService _brandService;

    public BrandController(ILogger<BrandController> logger, IBrandService brandService)
    {
        _logger = logger;
        _brandService = brandService;
    }

    /// <summary>
    /// get Active brands
    /// </summary>
    /// <returns>IEnumerable<BrandResponse> </returns>
    [HttpGet]
    public IEnumerable<BrandResponse> Get()
    {
        return _brandService.GetBrands();
    }

    /// <summary>
    /// create new brand
    /// </summary>
    /// <param name="brandRequest"></param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    public IActionResult Post(BrandRequest brandRequest)
    {

        // Validate request
        if (brandRequest == null || string.IsNullOrWhiteSpace(brandRequest.Name))
        {
            return BadRequest("Invalid Brand Name");
        }

        bool created = _brandService.CreateBrand(brandRequest, out string message);

        if (!created)
        {
            return BadRequest(message);
        }

        return Ok();

    }

    /// <summary>
    /// sets active to false on a brand
    /// </summary>
    /// <param name="Id"></param>
    /// <returns>IActionResult</returns>
    [HttpDelete]
    public IActionResult Delete(int Id)
    {
        bool deleted = _brandService.DeleteBrand(Id, out string message);

        if (!deleted)
        {
            return BadRequest(message);
        }

        return Ok();
    }

}

