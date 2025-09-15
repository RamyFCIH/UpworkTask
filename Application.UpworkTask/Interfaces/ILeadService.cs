using Application.UpworkTask.Dtos;
using Domain.UpworkTask.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UpworkTask.Interfaces
{
    public interface ILeadService
    {
        Task<Lead> AddLeadAsync(CreateLeadDto lead);
        Task<Lead?> GetLeadByIdAsync(int id);
        Task<IEnumerable<Lead>> GetAllLeadsAsync();
        Task<bool> DeleteLeadAsync(int id);
    }
}
