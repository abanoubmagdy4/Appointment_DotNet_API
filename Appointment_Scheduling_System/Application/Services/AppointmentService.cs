using Appointment_Scheduling_System.Application.DTOs;
using Appointment_Scheduling_System.Application.Helpers;
using Appointment_Scheduling_System.Application.Interfaces.Services;
using Appointment_Scheduling_System.Application.Interfaces.Repositories;
using Appointment_Scheduling_System.Infrastructure.Persistence.Repositories;
using Appointment_Scheduling_System.Application.Helpers.AutoMapper;

namespace Appointment_Scheduling_System.Application.Services
{
    public class AppointmentService : BaseService<Appointment>, IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public AppointmentService(IBaseRepository<Appointment> repository, IUnitOfWork unitOfWork , IMapper mapper)
        :base(repository, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ServiceResult<AppointmentResponseDto>> AddAppointmentAsync(AppointmentAddRequestDto addRequestDto)
        {
            try
            {
                if (addRequestDto == null)
                    return ServiceResult<AppointmentResponseDto>.Fail("Invalid request data.");

                DateTime startTime = addRequestDto.CreatedDate;

                if (startTime < DateTime.Now)
                    return ServiceResult<AppointmentResponseDto>.Fail("Cannot book an appointment in the past.");

                int minutesSlot = (startTime.Minute / 15) * 15; 
                DateTime slotStart = new DateTime(startTime.Year, startTime.Month, startTime.Day, startTime.Hour, minutesSlot, 0);
                DateTime slotEnd = slotStart.AddMinutes(15);

                bool slotTaken = await _unitOfWork.Appointment
                    .AnyAsync(a => a.CreatedDate >= slotStart && a.CreatedDate < slotEnd);

                if (slotTaken)
                    return ServiceResult<AppointmentResponseDto>.Fail("This 15-minute slot is already booked. Please choose another.");

                
                var appointment = mapper.Map<Appointment>(addRequestDto);

                await _unitOfWork.Appointment.AddAsync(appointment);
                await _unitOfWork.SaveChangesAsync();

                var response = mapper.Map<AppointmentResponseDto>(appointment);
                return ServiceResult<AppointmentResponseDto>.Ok(response);
            }
            catch (DbUpdateException dbEx)
            {
                return ServiceResult<AppointmentResponseDto>.Fail($"Database error: {dbEx.Message}");
            }
            catch (AutoMapperMappingException mapEx)
            {
                return ServiceResult<AppointmentResponseDto>.Fail($"Mapping error: {mapEx.Message}");
            }
            catch (Exception ex)
            {
                return ServiceResult<AppointmentResponseDto>.Fail($"An unexpected error occurred: {ex.Message}");
            }
        }

        public async Task<ServiceResult<AppointmentResponseDto>> UpdateAppointmentAsync(int id, UpdateAppointmentDto updateAppointmentDto)
        {
            try
            {
                if (updateAppointmentDto == null)
                    return ServiceResult<AppointmentResponseDto>.Fail("Invalid request data.");

                var appointment = await _unitOfWork.Appointment.GetByIdAsync(id);
                if (appointment == null)
                    return ServiceResult<AppointmentResponseDto>.Fail($"Appointment with ID {id} not found.");

                if (appointment.appointmentStatus.ToString() != "Scheduled")
                    return ServiceResult<AppointmentResponseDto>.Fail("Only Scheduled appointments can be updated.");

                DateTime newStartTime = updateAppointmentDto.CreatedDate;

                if (newStartTime < DateTime.Now)
                    return ServiceResult<AppointmentResponseDto>.Fail("Cannot book an appointment in the past.");

                int minutesSlot = (newStartTime.Minute / 15) * 15;
                DateTime slotStart = new DateTime(newStartTime.Year, newStartTime.Month, newStartTime.Day, newStartTime.Hour, minutesSlot, 0);
                DateTime slotEnd = slotStart.AddMinutes(15);

                bool slotTaken = await _unitOfWork.Appointment
                    .AnyAsync(a => a.Id != id && a.CreatedDate >= slotStart && a.CreatedDate < slotEnd);

                if (slotTaken)
                    return ServiceResult<AppointmentResponseDto>.Fail("This 15-minute slot is already booked. Please choose another.");

                mapper.Map(updateAppointmentDto, appointment);

                await _unitOfWork.Appointment.UpdateAsync(appointment);
                await _unitOfWork.SaveChangesAsync();

                var response = mapper.Map<AppointmentResponseDto>(appointment);
                return ServiceResult<AppointmentResponseDto>.Ok(response);
            }
            catch (DbUpdateException dbEx)
            {
                return ServiceResult<AppointmentResponseDto>.Fail($"Database error: {dbEx.Message}");
            }
            catch (AutoMapperMappingException mapEx)
            {
                return ServiceResult<AppointmentResponseDto>.Fail($"Mapping error: {mapEx.Message}");
            }
            catch (Exception ex)
            {
                return ServiceResult<AppointmentResponseDto>.Fail($"An unexpected error occurred: {ex.Message}");
            }
        }







    }


}
