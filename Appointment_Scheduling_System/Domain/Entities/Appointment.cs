using Appointment_Scheduling_System.Domain.Enums;

namespace Appointment_Scheduling_System.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public AppointmentStatus appointmentStatus { get; set; }

        public string Notes { get; set; }

    }
}
