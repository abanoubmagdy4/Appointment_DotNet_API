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
                {
                    return ServiceResult<AppointmentResponseDto>.Fail("Invalid request data.");
                }

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
                {
                    return ServiceResult<AppointmentResponseDto>.Fail("Invalid request data.");
                }

                var appointment = await _unitOfWork.Appointment.GetByIdAsync(id);
                if (appointment == null)
                {
                    return ServiceResult<AppointmentResponseDto>.Fail($"Appointment with ID {id} not found.");
                }

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
