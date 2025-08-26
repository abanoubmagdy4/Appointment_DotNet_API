using Microsoft.EntityFrameworkCore;

namespace Appointment_Scheduling_System.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IAppointmentRepository Appointment { get; }
         Task<int> SaveChangesAsync();
         void Dispose();
    }
}
