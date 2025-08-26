using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Appointment_Scheduling_System.Application.Interfaces.Repositories
{
    public interface IAppointmentRepository :IBaseRepository<Appointment>   
    {
        Task<bool> AnyAsync(Expression<Func<Appointment, bool>> predicate);
      
    }
}
