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
    public async Task<IEnumerable<HeroesResponse>> Get()
    {
        return await _heroService.GetHeroesAsync();
    }

    /// <summary>
    /// add new hero
    /// </summary>
    /// <param name="heroRequest"></param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    public async Task<IActionResult> Post(HeroRequest heroRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result =await _heroService.CreateHeroAsync(heroRequest);

        if(!result.Succeed)
        {
            return BadRequest(result.Message);
        }
      
        return Ok();

    }

    /// <summary>
    /// set hero to inactive
    /// </summary>
    /// <param name="id"></param>
    /// <returns>IActionResult</returns>
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {

        var result = await _heroService.DeleteHeroAsync(id);

        if(!result.Succeed)
        {
            return BadRequest(result.Message);
        }

        return Ok();
    }
}

