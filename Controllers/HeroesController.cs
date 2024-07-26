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

    [HttpGet]
    public IEnumerable<HeroesResponse> Get()
    {
        return _sampleContext.Heroes.Where(x=>x.IsActive==true).Select(i => new HeroesResponse
        {
            Alias = i.Alias,
            BrandName = i.Brand.Name,
            Id = i.Id,
            Name = i.Name
        }).ToList();

    }
    [HttpPost]
    public IActionResult Post(HeroRequest heroRequest)
    {
        Brand? brand = _sampleContext.GetBrandByName(heroRequest.BrandName);

        if (brand == null)
        {
            return BadRequest("Brand Not Found");
        }

        _sampleContext.Heroes.Add(new Hero { Alias = heroRequest.Alias, Name = heroRequest.Name, IsActive = true, BrandId = brand.Id });
        _sampleContext.SaveChanges();

        return Ok();
   

    }
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var hero = _sampleContext.Heroes.Where(x => x.Id == id && x.IsActive == true).FirstOrDefault();

        if (hero == null)
            return NotFound("valid hero with that id not found");

        hero.IsActive = false;
        _sampleContext.Update(hero);
        _sampleContext.SaveChanges();
        return Ok();

    }
}

