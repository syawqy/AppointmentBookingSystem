using AppointmentBookingSystem.Domain;
using AppointmentBookingSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Get appointments for a specific date
        /// </summary>
        /// <param name="date">The date to retrieve appointments for</param>
        /// <returns>A list of appointments for the specified date</returns>
        /// <response code="200">Returns the list of appointments</response>
        /// <response code="400">If the date is invalid</response>
        [HttpGet("{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAppointmentsForDate(DateTime date)
        {
            var appointments = await _appointmentService.GetAppointmentsForDate(date);
            return Ok(appointments);
        }

        /// <summary>
        /// Book a new appointment
        /// </summary>
        /// <param name="customerId">The ID of the customer</param>
        /// <param name="date">The requested appointment date</param>
        /// <returns>The newly created appointment</returns>
        /// <response code="200">Returns the newly created appointment</response>
        /// <response code="400">If the customer ID is invalid or the date is not available</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BookAppointment(int customerId, DateTime date)
        {
            try
            {
                var appointment = await _appointmentService.BookAppointment(customerId, date);
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
