namespace PrimerParciaL1.Models
{
    public class Event
    {
        public int Id { get; set; }                     // Clave primaria
        public string Title { get; set; }               // Título del evento
        public string Location { get; set; }            // Lugar del evento
        public DateTime StartAt { get; set; }           // Fecha y hora de inicio
        public DateTime? EndAt { get; set; }            // Fecha y hora de finalización (opcional)
        public bool IsOnline { get; set; }              // Si el evento es en línea o presencial
        public string? Notes { get; set; }              // Notas adicionales (opcional)
    }
}