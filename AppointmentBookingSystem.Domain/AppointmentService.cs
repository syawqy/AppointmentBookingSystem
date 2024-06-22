using AppointmentBookingSystem.Data.Entities;
using AppointmentBookingSystem.Domain.Interfaces;
using AppointmentBookingSystem.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingSystem.Domain
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAgencyRepository _agencyRepository;
        public AppointmentService(
            IAppointmentRepository appointmentRepository, 
            ICustomerRepository customerRepository,
            IAgencyRepository agencyRepository) 
        {
            _appointmentRepository = appointmentRepository;
            _customerRepository = customerRepository;
            _agencyRepository = agencyRepository;
        }

        public async Task<Appointment> BookAppointment(string name, string email, DateTime date)
        {
            var agency = await _agencyRepository.GetAgencyById(1); //for example

            if (IsOffDay(agency, date))
            {
                throw new Exception("Agency is closed on the selected date.");
            }

            while (IsOffDay(agency, date) || await IsFullDay(agency, date))
            {
                date = date.AddDays(1);
            }

            var appointment = new Appointment
            {
                Name = name,
                Email = email,
                Date = date,
            };

            return await _appointmentRepository.CreateAppointment(appointment);
        }

        private async Task<bool> IsFullDay(Agency agency, DateTime date)
        {
            var data =  await _appointmentRepository.CountAppointmentsForDate(date.Date) >= agency.MaxAppointmentsPerDay;
            return data;
        }

        private bool IsOffDay(Agency agency, DateTime date)
        {
            if (agency.OffDays.Contains(date.Date))
            {
                return true;
            }

            return date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsForDate(DateTime date)
        {
            return await _appointmentRepository.GetAppointmentsForDate(date);
        }
    }
}
