using AppointmentBookingSystem.Data.Entities;
using AppointmentBookingSystem.Data.Interfaces;
using AppointmentBookingSystem.Domain;
using Moq;
using System;

namespace AppointmentBookingSystem.Test
{
    public class AppointmentServiceTests
    {
        private Mock<IAppointmentRepository> _mockAppointmentRepository;
        private Mock<ICustomerRepository> _mockCustomerRepository;
        private Mock<IAgencyRepository> _mockAgencyRepository;
        private AppointmentService _appointmentService;

        [SetUp]
        public void Setup()
        {
            _mockAppointmentRepository = new Mock<IAppointmentRepository>();
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockAgencyRepository = new Mock<IAgencyRepository>();

            _appointmentService = new AppointmentService(
                _mockAppointmentRepository.Object,
                _mockCustomerRepository.Object,
                _mockAgencyRepository.Object);
        }

        [Test]
        public async Task GetAppointmentsForDate_ShouldReturnAppointmentsForGivenDate()
        {
            // Arrange
            var date = new DateTime(2023, 6, 20);
            var appointments = new List<Appointment>
            {
                new Appointment { Id = 1, Name = "john", Email = "john@gmail.com", Date = date},
                new Appointment { Id = 2, Name = "Nash", Email = "nash@gmail.com", Date = date},
            };
            _mockAppointmentRepository.Setup(r => r.GetAppointmentsForDate(date))
                .ReturnsAsync(appointments);

            // Act
            var result = await _appointmentService.GetAppointmentsForDate(date);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(appointments.Count));
        }

        [Test]
        public async Task BookAppointment_ShouldBookAppointmentForValidCustomer()
        {
            // Arrange
            var customerId = 1;
            var date = new DateTime(2024, 6, 20);
            var agency = new Agency { Id = 1, Name = "Test Agency", MaxAppointmentsPerDay = 5, OffDays = new List<DateTime>() };
            var appointment = new Appointment { Id = 1, Name = "John Doe", Email = "john@example.com", Date = date};

            _mockAgencyRepository.Setup(r => r.GetAgencyById(1))
                .ReturnsAsync(agency);
            _mockAppointmentRepository.Setup(r => r.GetAppointmentsForDate(date))
                .ReturnsAsync(new List<Appointment>());
            _mockAppointmentRepository.Setup(r => r.CreateAppointment(It.IsAny<Appointment>()))
                .ReturnsAsync((Appointment a) => a);

            // Act
            var result = await _appointmentService.BookAppointment(appointment.Name, appointment.Email, date);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(appointment.Name));
            Assert.That(result.Date, Is.EqualTo(date));
        }

        [Test]
        public async Task BookAppointment_ShouldReturnErrorBecauseOffDays()
        {
            // Arrange
            var customerId = 1;
            var date = new DateTime(2024, 6, 20);
            var agency = new Agency { Id = 1, Name = "Test Agency", MaxAppointmentsPerDay = 5, OffDays = new List<DateTime>() };
            agency.OffDays.Add(date);

            var appointment = new Appointment { Id = 1, Name = "John Doe", Email = "john@example.com", Date = date };

            _mockAgencyRepository.Setup(r => r.GetAgencyById(1))
                .ReturnsAsync(agency);
            _mockAppointmentRepository.Setup(r => r.GetAppointmentsForDate(date))
                .ReturnsAsync(new List<Appointment>());
            _mockAppointmentRepository.Setup(r => r.CreateAppointment(It.IsAny<Appointment>()))
                .ReturnsAsync((Appointment a) => a);

            // Act
            var exception = Assert.ThrowsAsync<Exception>(async () =>
                await _appointmentService.BookAppointment(appointment.Name, appointment.Email, date));

            // Assert
            Assert.That(exception.Message, Is.EqualTo("Agency is closed on the selected date."));
        }

        [Test]
        public async Task BookAppointment_ShouldReturnNexWorkingDayBecauseFull()
        {
            // Arrange
            var customerId = 1;
            var date = new DateTime(2024, 6, 21);
            var expectedDate = new DateTime(2024, 6, 24);
            var agency = new Agency { Id = 1, Name = "Test Agency", MaxAppointmentsPerDay = 1, OffDays = new List<DateTime>() };

            var appointment = new Appointment { Id = 1, Name = "John Doe", Email = "john@example.com", Date = date };

            _mockAgencyRepository.Setup(r => r.GetAgencyById(1))
                .ReturnsAsync(agency);
            _mockAppointmentRepository.Setup(r => r.CountAppointmentsForDate(date))
                .ReturnsAsync(1);
            _mockAppointmentRepository.Setup(r => r.CreateAppointment(It.IsAny<Appointment>()))
                .ReturnsAsync((Appointment a) => a);

            // Act
            var result = await _appointmentService.BookAppointment(appointment.Name, appointment.Email, date);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(appointment.Name));
            Assert.That(result.Date, Is.EqualTo(expectedDate));
        }
    }
}