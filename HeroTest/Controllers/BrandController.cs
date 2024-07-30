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
    public async Task<IEnumerable<BrandResponse>> Get()
    {
        return await _brandService.GetBrandsAsync();
    }

    /// <summary>
    /// create new brand
    /// </summary>
    /// <param name="brandRequest"></param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    public async Task<IActionResult> Post(BrandRequest brandRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validate request
        if (brandRequest == null || string.IsNullOrWhiteSpace(brandRequest.Name))
        {
            return BadRequest("Invalid Brand Name");
        }

        var result = await _brandService.CreateBrandAsync(brandRequest);

        if (!result.Succeed)
        {
            return BadRequest(result.Message);
        }

        return Ok();

    }

    /// <summary>
    /// sets active to false on a brand
    /// </summary>
    /// <param name="Id"></param>
    /// <returns>IActionResult</returns>
    [HttpDelete]
    public async Task<IActionResult> Delete(int Id)
    {
        var result = await _brandService.DeleteBrandAsync(Id);

        if (!result.Succeed)
        {
            return BadRequest(result.Message);
        }

        return Ok();
    }

}

