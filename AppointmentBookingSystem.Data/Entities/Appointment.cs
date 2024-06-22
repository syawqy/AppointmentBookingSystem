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
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// The customer name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// The customer email
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// The date and time of the appointment
        /// </summary>
        [Required]
        public DateTime Date { get; set; }
    }
}
