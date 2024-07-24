using System;
using Pinewood.Customer.Application.Core.DTOs;
using Pinewood.Customer.Application.Core.Models;

namespace Pinewood.Customer.Application.Core.Interfaces
{
	public interface ICustomerInfoRepository
	{
		public Task SaveCustomer(CustomerInfoModel customerInfo);

		public Task DeleteCustomer(Guid id);

        public Task<List<CustomerInfoModel>> GetAllCustomers();

        public Task<CustomerInfoModel> GetCustomerById(Guid id);

        public Task<IEnumerable<CustomerInfoModel>> GetCustomerByEmail(string email);


    }
}

