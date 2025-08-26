
namespace Appointment_Scheduling_System.Application.Interfaces.Services
{
    public interface IServiceUnitOfWork
    {
        IAppointmentService Appointment { get; }
    }
}
