using AppointmentBookingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingSystem.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                if (context.Agencies.Any())
                {
                    return;   
                }

                context.Agencies.AddRange(
                    new Agency
                    {
                        Name = "Main Street Agency",
                        MaxAppointmentsPerDay = 5,
                        OffDays = new List<DateTime>
                        {
                            new DateTime(2023, 7, 4), 
                            new DateTime(2023, 12, 25) 
                        }
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
