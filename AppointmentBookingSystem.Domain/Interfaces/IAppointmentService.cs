using AppointmentBookingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingSystem.Domain.Interfaces
{
    public interface IAppointmentService
    {
        public Task<IEnumerable<Appointment>> GetAppointmentsForDate(DateTime date);
        public Task<Appointment> BookAppointment(int customerId, DateTime date);
    }
}
