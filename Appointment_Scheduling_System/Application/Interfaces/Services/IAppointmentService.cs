using Appointment_Scheduling_System.Application.DTOs;
using Appointment_Scheduling_System.Application.Helpers;

namespace Appointment_Scheduling_System.Application.Interfaces.Services
{
    public interface IAppointmentService : IBaseService<Appointment>
    {
        Task<ServiceResult<AppointmentResponseDto>> AddAppointmentAsync(AppointmentAddRequestDto addRequestDto);
        Task<ServiceResult<AppointmentResponseDto>> UpdateAppointmentAsync(int id, UpdateAppointmentDto updateAppointmentDto);
    }
}
