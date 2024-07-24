using Amazon.DynamoDBv2.DataModel;
using Pinewood.Customer.Application.Core.Interfaces;
using Pinewood.Customer.Application.Core.Models;

namespace Pinewood.Customer.Infrastructure.Repositories
{
    public class CustomerInfoRepository : ICustomerInfoRepository
	{
        private readonly IDynamoDBContext _context;

        public CustomerInfoRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task DeleteCustomer(Guid id) =>

            await _context.DeleteAsync<CustomerInfoModel>(id);


        public async Task<List<CustomerInfoModel>> GetAllCustomers() =>

            await _context.ScanAsync<CustomerInfoModel>(new List<ScanCondition>()).GetRemainingAsync();


        public async Task<IEnumerable<CustomerInfoModel>> GetCustomerByEmail(string email) { 

         return await _context.QueryAsync<CustomerInfoModel>(email,
                new DynamoDBOperationConfig { IndexName = "email-index" }).GetRemainingAsync();
        }

        public async Task<CustomerInfoModel> GetCustomerById(Guid id) =>

            await _context.LoadAsync<CustomerInfoModel>(id);


        public async Task SaveCustomer(CustomerInfoModel customerInfo) =>

            await _context.SaveAsync(customerInfo);
    }
}

