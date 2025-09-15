using Application.UpworkTask.Dtos;
using Domain.UpworkTask.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UpworkTask.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> AddCustomerAsync(CreateCustomerDto customer);
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<bool> DeleteCustomerAsync(int id);
    }
}
