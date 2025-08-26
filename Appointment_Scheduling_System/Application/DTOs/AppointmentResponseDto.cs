namespace Appointment_Scheduling_System.Application.DTOs
{
    public class AppointmentResponseDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public AppointmentStatus AppointmentStatus { get; set; }

        public string? Notes { get; set; }
    }
}
