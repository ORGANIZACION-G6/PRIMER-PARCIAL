using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerParcial1.Data;
using PrimerParcial1.Models;

namespace PrimerParcial1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupportTicketsController : ControllerBase
{
    private readonly AppDbContext _db;
    public SupportTicketsController(AppDbContext db) => _db = db;

    // GET: /api/SupportTickets?status=Open&severity=High&email=alguien@dominio.com
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupportTicket>>> GetAll(
        [FromQuery] TicketStatus? status,
        [FromQuery] TicketSeverity? severity,
        [FromQuery] string? email)
    {
        var q = _db.SupportTickets.AsNoTracking().AsQueryable();

        if (status is not null)   q = q.Where(t => t.Status == status);
        if (severity is not null) q = q.Where(t => t.Severity == severity);
        if (!string.IsNullOrWhiteSpace(email)) q = q.Where(t => t.RequesterEmail == email);

        var list = await q.OrderByDescending(t => t.OpenedAt).ToListAsync();
        return Ok(list);
    }

    // GET: /api/SupportTickets/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SupportTicket>> GetById(int id)
    {
        var t = await _db.SupportTickets.FindAsync(id);
        return t is null ? NotFound() : Ok(t);
    }

    // POST: /api/SupportTickets  (crea: OpenedAt=UtcNow, Status=Open)
    [HttpPost]
    public async Task<ActionResult<SupportTicket>> Create(SupportTicket input)
    {
        input.Id = 0;
        input.OpenedAt = DateTime.UtcNow;
        input.Status = TicketStatus.Open;
        input.ClosedAt = null;

        _db.SupportTickets.Add(input);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
    }

    // PUT: /api/SupportTickets/5  (actualiza; maneja ClosedAt según Status)
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, SupportTicket input)
    {
        if (id != input.Id) return BadRequest("El id de la ruta no coincide con el del cuerpo.");

        var t = await _db.SupportTickets.FindAsync(id);
        if (t is null) return NotFound();

        t.Subject       = input.Subject;
        t.RequesterEmail= input.RequesterEmail;
        t.Description   = input.Description;
        t.Severity      = input.Severity;
        t.AssignedTo    = input.AssignedTo;

        if (t.Status != input.Status)
        {
            t.Status  = input.Status;
            t.ClosedAt = t.Status == TicketStatus.Closed ? DateTime.UtcNow : null;
        }

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // PATCH: /api/SupportTickets/5/close
    [HttpPatch("{id:int}/close")]
    public async Task<IActionResult> Close(int id)
    {
        var t = await _db.SupportTickets.FindAsync(id);
        if (t is null) return NotFound();

        t.Status   = TicketStatus.Closed;
        t.ClosedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: /api/SupportTickets/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var t = await _db.SupportTickets.FindAsync(id);
        if (t is null) return NotFound();

        _db.SupportTickets.Remove(t);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
