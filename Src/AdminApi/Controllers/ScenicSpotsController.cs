using Juzhen.Domain.Aggregates;
using Juzhen.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ScenicSpotsController : ControllerBase
{
    private readonly IScenicSpotRepository _repository;

    public ScenicSpotsController(IScenicSpotRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ScenicSpot scenicSpot)
    {
        scenicSpot.Id = 0; // 显式地设置为0，通常这是默认值，但这里是为了清晰说明

        var created = await _repository.AddAsync(scenicSpot);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var scenicSpot = await _repository.GetByIdAsync(id);
        if (scenicSpot == null) return NotFound();
        return Ok(scenicSpot);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var scenicSpots = await _repository.GetAllAsync();
        return Ok(scenicSpots);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ScenicSpot scenicSpot)
    {
        if (id != scenicSpot.Id) return BadRequest("ID mismatch");
        await _repository.UpdateAsync(scenicSpot);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
