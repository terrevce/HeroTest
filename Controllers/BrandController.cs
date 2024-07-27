using HeroTest.Models;
using HeroTest.Models.RequestModels;
using HeroTest.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace HeroTest.Controllers;
[ApiController]
[Route("[controller]")]
public class BrandController : ControllerBase
{

    private readonly ILogger<BrandController> _logger;
    private readonly SampleContext _sampleContext;

    public BrandController(ILogger<BrandController> logger, SampleContext sampleContext)
    {
        _logger = logger;
        _sampleContext = sampleContext;
    }

    /// <summary>
    /// get Active brands
    /// </summary>
    /// <returns>IEnumerable<BrandResponse> </returns>
    [HttpGet]  
    public IEnumerable<BrandResponse> Get()
    {
        return _sampleContext.Brands.Where(x => x.IsActive == true).Select(i => new BrandResponse
        {
            Name = i.Name,
            Id = i.Id
        }).ToList();
    }

    /// <summary>
    /// create new brand
    /// </summary>
    /// <param name="brandRequest"></param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    public IActionResult Post(BrandRequest brandRequest)
    {
        try
        {
            // Validate request
            if (brandRequest == null || string.IsNullOrWhiteSpace(brandRequest.Name))
            {
                return BadRequest("Invalid Brand Name");
            }

            //check existing
            var brand = _sampleContext.GetBrandByName(brandRequest.Name);
            if (brand != null)
            {
                return Conflict(brand);
            }

            _sampleContext.Brands.Add(new Brand { Name = brandRequest.Name, IsActive = true });
            _sampleContext.SaveChanges();
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException?.Message);
            return Problem(detail: ex.Message, statusCode:500);
            
        }

    }

    /// <summary>
    /// sets active to false on a brand
    /// </summary>
    /// <param name="Id"></param>
    /// <returns>IActionResult</returns>
    [HttpDelete]
    public IActionResult Delete(int Id)
    {
        try
        {
            //check existing
            var brand = _sampleContext.GetBrandById(Id);
            if (brand == null)
            {
                return NotFound("Active Brand Not Found");
            }

            //set inactive
            brand.IsActive = false;
            _sampleContext.Update(brand);
            _sampleContext.SaveChanges();
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException?.Message);
            return Problem(detail: ex.Message, statusCode: 500);

        }

    }
}

