using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace API;

[ApiController]
[Route("/")]
public class SomeController : ControllerBase
{
    private readonly Context _context;
    private readonly DbSet<SomeEntity> _dbSet;

    public SomeController(Context context)
    {
        _context = context;
        _dbSet = _context.Set<SomeEntity>();
    }

    public static string GenerateUniqueName()
    {
        return $"name-{Guid.NewGuid()}";
    }

    [HttpGet("get-one")]
    public async Task<IActionResult> GetOne()
    {
        try
        {
            var mockEntity = await _dbSet.FirstOrDefaultAsync();

            if (mockEntity is null) return NotFound();

            return Ok(new { entity = mockEntity });
        }
        catch (System.Exception)
        {
            return StatusCode(500, new { message = "InternalServerError" });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var mockEntities = await _dbSet.Take(10).ToListAsync();

            var total = await _dbSet.CountAsync();

            return Ok(new { list = mockEntities, total });
        }
        catch (System.Exception)
        {
            return StatusCode(500, new { message = "InternalServerError" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        try
        {
            var mockEntity = new SomeEntity() { Name = GenerateUniqueName() };

            await _context.SomeEntities.AddAsync(mockEntity);

            await _context.SaveChangesAsync();

            return Ok(new { entity = mockEntity });
        }
        catch
        {
            return StatusCode(500, new { message = "InternalServerError" });
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update()
    {
        try
        {
            var mockEntity = await _dbSet.FirstOrDefaultAsync();

            if (mockEntity is null) return NotFound();

            mockEntity.Name = GenerateUniqueName();

            _dbSet.Update(mockEntity);

            await _context.SaveChangesAsync();

            return Ok(new { entity = mockEntity });
        }
        catch (System.Exception)
        {
            return StatusCode(500, new { message = "InternalServerError" });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        try
        {
            var mockEntity = await _dbSet.FirstOrDefaultAsync();

            if (mockEntity is null) return NotFound();

            _dbSet.Remove(mockEntity);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (System.Exception)
        {
            return StatusCode(500, new { message = "InternalServerError" });
        }
    }
}