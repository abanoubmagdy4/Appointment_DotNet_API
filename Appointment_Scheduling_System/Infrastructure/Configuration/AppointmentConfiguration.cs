using Appointment_Scheduling_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
               .ValueGeneratedOnAdd(); 

        builder.Property(a => a.CustomerName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(a => a.CreatedDate)
               .IsRequired();

        builder.Property(a => a.appointmentStatus)
               .IsRequired()
               .HasConversion<string>();

        builder.Property(a => a.Notes)
               .HasMaxLength(500);
    }
}
