using AppointmentBookingSystem.Data.Entities;
using AppointmentBookingSystem.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingSystem.Data.Repositories
{
    public class AgencyRepository : IAgencyRepository
    {
        private readonly DataContext _context;

        public AgencyRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Agency> CreateAgency(Agency agency)
        {
            _context.Agencies.Add(agency);
            await _context.SaveChangesAsync();
            return agency;
        }

        public async Task DeleteAgency(int id)
        {
            var agency = await _context.Agencies.FindAsync(id);
            if (agency != null)
            {
                _context.Agencies.Remove(agency);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Agency> GetAgencyById(int id)
        {
            return await _context.Agencies
                        .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Agency> UpdateAgency(Agency agency)
        {
            _context.Entry(agency).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return agency;
        }
    }
}
