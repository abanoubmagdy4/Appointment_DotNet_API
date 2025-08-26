using Appointment_Scheduling_System.Application.Interfaces.Repositories;

namespace Appointment_Scheduling_System.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _Context;
        private readonly IServiceProvider _ServiceProvider;

        public UnitOfWork(AppDbContext context, IServiceProvider serviceProvider)
        {

            _ServiceProvider = serviceProvider;
            _Context = context;
        }
        private IAppointmentRepository _appointment;
        public IAppointmentRepository Appointment => _appointment ??= _ServiceProvider.GetRequiredService<IAppointmentRepository>();
      
        public Task<int> SaveChangesAsync() => _Context.SaveChangesAsync();

        public void Dispose()
        {
            _Context.Dispose();
        }
    }
}
