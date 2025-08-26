namespace Appointment_Scheduling_System.Application.DTOs
{
    public class UpdateAppointmentDto
    {
        [Required(ErrorMessage = "Customer name is required")]
        [MaxLength(100, ErrorMessage = "Customer name cannot exceed 100 characters")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date and time are required")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [EnumDataType(typeof(AppointmentStatus), ErrorMessage = "Invalid status value")]
        public AppointmentStatus appointmentStatus { get; set; }

        [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }
    }
}
