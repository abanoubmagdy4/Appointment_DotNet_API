using Appointment_Scheduling_System.Application.Helpers.AutoMapper;
using Appointment_Scheduling_System.Application.Interfaces.Repositories;
using Appointment_Scheduling_System.Application.Interfaces.Services;
using Appointment_Scheduling_System.Application.Services;
using Appointment_Scheduling_System.Infrastructure.Persistence;
using Appointment_Scheduling_System.Infrastructure.Persistence.Repositories;
using Appointment_Scheduling_System.Infrastructure.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment_Scheduling_System.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            //Services Register
            builder.Services.AddScoped<IServiceUnitOfWork, ServiceUnitOfWork>();
            builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();


            //Repositories Register
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //AutoMapperRegister
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AppointmentMappingProfile>();
            });

            // USe Cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularClient",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAngularClient");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
