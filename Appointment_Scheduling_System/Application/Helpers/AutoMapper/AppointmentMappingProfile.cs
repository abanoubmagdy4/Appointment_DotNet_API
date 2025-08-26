
using Appointment_Scheduling_System.Application.DTOs;

namespace Appointment_Scheduling_System.Application.Helpers.AutoMapper
{
    public class AppointmentMappingProfile :Profile 
    {
        public AppointmentMappingProfile()
        {
            //Add
            CreateMap<AppointmentAddRequestDto, Appointment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<Appointment, AppointmentAddRequestDto>();

            //Update
            CreateMap<UpdateAppointmentDto, Appointment>()
                       .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()); 

            CreateMap<Appointment, UpdateAppointmentDto>();


            //Response
            CreateMap<Appointment, AppointmentResponseDto>();
            CreateMap<AppointmentResponseDto, Appointment>();


        }

    }
}
