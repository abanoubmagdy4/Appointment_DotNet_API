using Appointment_Scheduling_System.Application.Interfaces.Repositories;
using Appointment_Scheduling_System.Infrastructure.Persistence.Repositories;

namespace Appointment_Scheduling_System.Infrastructure.Persistence.Repository
{
    public class AppointmentRepository :BaseRepository<Appointment>,IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context)
                : base(context)   
        {

        }        
            
        }
    }

