using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingSystem.Data.Entities
{
    public class Appointment
    {
        /// <summary>
        /// The unique identifier for the appointment
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// The ID of the customer who booked the appointment
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// The customer who booked the appointment
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// The date and time of the appointment
        /// </summary>
        [Required]
        public DateTime Date { get; set; }
    }
}
