using AppointmentBookingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingSystem.Data.Interfaces
{
    public interface IAgencyRepository
    {
        Task<Agency> GetAgencyById(int id);
        Task<Agency> CreateAgency(Agency customer);
        Task<Agency> UpdateAgency(Agency customer);
        Task DeleteAgency(int id);
    }
}
