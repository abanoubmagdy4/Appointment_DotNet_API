using Appointment_Scheduling_System.Application.Interfaces.Repositories;
using Appointment_Scheduling_System.Infrastructure.Persistence.Repositories;
using System.Linq.Expressions;

namespace Appointment_Scheduling_System.Infrastructure.Persistence.Repository
{
    public class AppointmentRepository :BaseRepository<Appointment>,IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context)
                : base(context)   
        {

        }
        public async Task<bool> AnyAsync(Expression<Func<Appointment, bool>> predicate)
        {
            return await _context.Appointment.AnyAsync(predicate);
        }
    }
    }

