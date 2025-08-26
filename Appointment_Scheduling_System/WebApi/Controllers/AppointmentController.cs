using Appointment_Scheduling_System.Application.DTOs;
using Appointment_Scheduling_System.Application.Helpers;
using Appointment_Scheduling_System.Application.Interfaces.Repositories;
using Appointment_Scheduling_System.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Appointment_Scheduling_System.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;

        public AppointmentController(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<List<Appointment>>>> GetAll()
        {
            try
            {
                var serviceResult = await _serviceUnitOfWork.Appointment.GetAllAsync();

                if (serviceResult.Data == null || !serviceResult.Data.Any())
                {
                    return NotFound(new GeneralResponse<List<Appointment>>
                    {
                        Success = false,
                        Message = "No appointments found",
                        Data = null
                    });
                }

                return Ok(new GeneralResponse<List<Appointment>>
                {
                    Success = true,
                    Message = "Appointments fetched successfully",
                    Data = serviceResult.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<List<Appointment>>
                {
                    Success = false,
                    Message = $"Internal server error: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<Appointment>>> GetById(int id)
        {
            try
            {
                var serviceResult = await _serviceUnitOfWork.Appointment.GetByIdAsync(id);

                if (serviceResult.Data == null)
                {
                    return NotFound(new GeneralResponse<Appointment>
                    {
                        Success = false,
                        Message = "Appointment not found",
                        Data = null
                    });
                }

                return Ok(new GeneralResponse<Appointment>
                {
                    Success = true,
                    Message = "Appointment fetched successfully",
                    Data = serviceResult.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<Appointment>
                {
                    Success = false,
                    Message = $"Internal server error: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<AppointmentResponseDto>>> AddAppointment(AppointmentAddRequestDto addRequestDto)
        {
            var serviceResult = await _serviceUnitOfWork.Appointment.AddAppointmentAsync(addRequestDto);

            if (!serviceResult.Success)
            {
                return BadRequest(new GeneralResponse<AppointmentResponseDto>
                {
                    Success = false,
                    Message = serviceResult.ErrorMessage,
                    Data = null
                });
            }

            return Ok(new GeneralResponse<AppointmentResponseDto>
            {
                Success = true,
                Message = "Appointment added successfully",
                Data = serviceResult.Data,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<AppointmentResponseDto>>> UpdateAppointment(int id, UpdateAppointmentDto updateAppointmentDto)
        {
            var serviceResult = await _serviceUnitOfWork.Appointment.UpdateAppointmentAsync(id, updateAppointmentDto);

            if (!serviceResult.Success)
            {
                return BadRequest(new GeneralResponse<AppointmentResponseDto>
                {
                    Success = false,
                    Message = serviceResult.ErrorMessage,
                    Data = null
                });
            }

            return Ok(new GeneralResponse<AppointmentResponseDto>
            {
                Success = true,
                Message = "Appointment updated successfully",
                Data = serviceResult.Data
            });
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAppointment(int id)
        {
            var serviceResult = await _serviceUnitOfWork.Appointment.DeleteAsync(id);

            if (!serviceResult.Success)
            {
                return BadRequest(new GeneralResponse<AppointmentResponseDto>
                {
                    Success = false,
                    Message = serviceResult.ErrorMessage ?? "Failed to delete the appointment.",
                    Data = null
                });
            }

            return Ok(new GeneralResponse<AppointmentResponseDto>
            {
                Success = true,
                Message = "Appointment deleted successfully.",
                Data = null
            });
        }

    }
}
