using System.ComponentModel.DataAnnotations;

namespace PrimerParcial1.Models;

public class SupportTicket
{
    public int Id { get; set; } // PK

    [Required, StringLength(200)]
    public string Subject { get; set; } = default!;

    [Required, EmailAddress, StringLength(200)]
    public string RequesterEmail { get; set; } = default!;

    public string? Description { get; set; } // nullable

    [Required]
    public TicketSeverity Severity { get; set; }

    [Required]
    public TicketStatus Status { get; set; } = TicketStatus.Open;

    public DateTime OpenedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ClosedAt { get; set; } // nullable

    public string? AssignedTo { get; set; } // nullable
}