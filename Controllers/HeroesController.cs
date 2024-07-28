using HeroTest.Models;
using HeroTest.Models.RequestModels;
using HeroTest.Models.ResponseModels;
using HeroTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeroTest.Controllers;
[ApiController]
[Route("[controller]")]
public class HeroesController : ControllerBase
{

    private readonly ILogger<HeroesController> _logger;
    private readonly IHeroService _heroService;

    public HeroesController(ILogger<HeroesController> logger, IHeroService heroService)
    {
        _logger = logger;
        _heroService = heroService;
    }

    /// <summary>
    /// get active heroes
    /// </summary>
    /// <returns>IEnumerable<HeroesResponse></returns>
    [HttpGet]
    public IEnumerable<HeroesResponse> Get()
    {
        return _heroService.GetHeroes();
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

        bool result = _heroService.CreateHero(heroRequest, out string message);

        if(result==false)
        {
            return BadRequest(message);
        }
      
        return Ok();

    }

    /// <summary>
    /// set hero to inactive
    /// </summary>
    /// <param name="id"></param>
    /// <returns>IActionResult</returns>
    [HttpDelete]
    public IActionResult Delete(int id)
    {

        bool result = _heroService.DeleteHero(id,out string message);

        if(result == false)
        {
            return BadRequest(message);
        }

        return Ok();
    }
}

