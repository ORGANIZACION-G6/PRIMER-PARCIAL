using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PrimerParciaL1.Models;
using PrimerParcialProgra.Data;


namespace PrimerParciaL1Progra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly AppDbContext _db;
    public EventController(AppDbContext db) => _db = db;

    // GET /api/event
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetAll()
        => await _db.Events.AsNoTracking().ToListAsync();

    // GET /api/event/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Event>> GetById(int id)
        => await _db.Events.FindAsync(id) is { } e ? e : NotFound();

    // POST /api/event
    [HttpPost]
    public async Task<ActionResult<Event>> Create(Event input)
    {
        _db.Events.Add(input);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
    }

    // PUT /api/event/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Event input)
    {
        if (id != input.Id) return BadRequest("Id de ruta y cuerpo no coinciden.");
        var exists = await _db.Events.AnyAsync(x => x.Id == id);
        if (!exists) return NotFound();

        _db.Entry(input).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE /api/event/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.Events.FindAsync(id);
        if (e is null) return NotFound();

        _db.Events.Remove(e);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}