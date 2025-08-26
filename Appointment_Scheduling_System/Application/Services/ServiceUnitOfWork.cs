using Appointment_Scheduling_System.Application.Interfaces.Services;
using Appointment_Scheduling_System.Application.Interfaces.Repositories;
using Appointment_Scheduling_System.Infrastructure.Persistence.Repositories;

namespace Appointment_Scheduling_System.Application.Services
{
    public class ServiceUnitOfWork : IServiceUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceUnitOfWork(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private IAppointmentService _appointment;
        public IAppointmentService Appointment => _appointment ??= _serviceProvider.GetRequiredService<IAppointmentService>();
    }
}
