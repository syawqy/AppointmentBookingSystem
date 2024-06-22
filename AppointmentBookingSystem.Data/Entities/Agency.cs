using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingSystem.Data.Entities
{
    public class Agency
    {
        /// <summary>
        /// The unique identifier for the agency
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// The agency name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Maximum appointment agency can take per day
        /// </summary>
        [Required]
        public int MaxAppointmentsPerDay { get; set; }
        /// <summary>
        /// Appointment can't be made on off days
        /// </summary>
        [Required]
        public List<DateTime> OffDays { get; set; }
    }
}
