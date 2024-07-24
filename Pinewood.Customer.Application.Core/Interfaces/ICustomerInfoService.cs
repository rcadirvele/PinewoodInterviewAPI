using System;
using Pinewood.Customer.Application.Core.DTOs;

namespace Pinewood.Customer.Application.Core.Interfaces
{
	public interface ICustomerInfoService
	{
		public Task<CustomerInfoDto> CreateCustomer(CustomerInfoDto customerInfoDto);

		public Task<CustomerInfoDto> UpdateCustomer(CustomerInfoDto customerInfoDto);

		public Task<Guid> DeleteCustomer(Guid id);

		public Task<List<CustomerInfoDto>> GetAllCustomers();

		public Task<CustomerInfoDto> GetCustomerById(Guid id);

    }
}

