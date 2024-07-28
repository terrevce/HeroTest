using HeroTest.Models;
using HeroTest.Models.RequestModels;
using HeroTest.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace HeroTest.Controllers;
[ApiController]
[Route("[controller]")]
public class HeroesController : ControllerBase
{

    private readonly ILogger<HeroesController> _logger;
    private readonly SampleContext _sampleContext;

    public HeroesController(ILogger<HeroesController> logger, SampleContext sampleContext)
    {
        _logger = logger;
        _sampleContext = sampleContext;
    }

    /// <summary>
    /// get active heroes
    /// </summary>
    /// <returns>IEnumerable<HeroesResponse></returns>
    [HttpGet]
    public IEnumerable<HeroesResponse> Get()
    {
        return _sampleContext.Heroes.Where(x => x.IsActive == true).Select(i => new HeroesResponse
        {
            Alias = i.Alias,
            BrandName = i.Brand.Name,
            Id = i.Id,
            Name = i.Name
        }).ToList();
    }

    /// <summary>
    /// add new hero
    /// </summary>
    /// <param name="heroRequest"></param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    public IActionResult Post(HeroRequest heroRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            //validate
            Brand? brand = _sampleContext.GetBrandByName(heroRequest.BrandName);

            if (brand == null)
            {
                return BadRequest("Brand Not Found");
            }

            _sampleContext.Heroes.Add(new Hero { Alias = heroRequest.Alias, Name = heroRequest.Name, IsActive = true, BrandId = brand.Id });
            _sampleContext.SaveChanges();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException?.Message);
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    /// <summary>
    /// set hero to inactive
    /// </summary>
    /// <param name="id"></param>
    /// <returns>IActionResult</returns>
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        try
        {
            var hero = _sampleContext.Heroes.Where(x => x.Id == id && x.IsActive == true).FirstOrDefault();

            if (hero == null)
                return NotFound("valid hero with that id not found");

            hero.IsActive = false;
            _sampleContext.Update(hero);
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

