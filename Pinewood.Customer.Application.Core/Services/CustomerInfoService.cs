using AutoMapper;
using Pinewood.Customer.Application.Core.DTOs;
using Pinewood.Customer.Application.Core.Interfaces;
using Pinewood.Customer.Application.Core.Models;
using Serilog;

namespace Pinewood.Customer.Application.Core.Services
{
    public class CustomerInfoService : ICustomerInfoService
    {
        private readonly ILogger _logger;

        private readonly ICustomerInfoRepository _customerInfoRepos;

        private readonly IMapper _mapper;

        public CustomerInfoService(ILogger logger, ICustomerInfoRepository customerInfoRepos, IMapper mapper)
        {
            _logger = logger;
            _customerInfoRepos = customerInfoRepos;
            _mapper = mapper;
        }


        public async Task<CustomerInfoDto> CreateCustomer(CustomerInfoDto customerInfoDto)
        {
            _logger.Information($"CreateCustomer for customer with first name - {customerInfoDto.FirstName}");
            var customerInfoModel = _mapper.Map<CustomerInfoModel>(customerInfoDto);
            customerInfoModel.CreatedDate = DateTime.Now;

            try
            {  
               await _customerInfoRepos.SaveCustomer(customerInfoModel);

               return customerInfoDto;
            }
            catch(Exception ex)
            {
                _logger.Error($"CreateCustomer - Error while creating new customer - {customerInfoDto.FirstName} {customerInfoDto.LastName} - {ex.InnerException}");

                throw;
            }
        }

        public async Task<Guid> DeleteCustomer(Guid id)
        {
            _logger.Information($"DeleteCustomer for customer id - {id}");

            try
            {
                var customer = await _customerInfoRepos.GetCustomerById(id);
                if(customer is null) {
                    _logger.Warning($"DeleteCustomer - Customer does not exists");
                    throw new Exception("DeleteCustomer - Customer does not exists");
                }

                await _customerInfoRepos.DeleteCustomer(id);

                return id;

            }
            catch(Exception ex)
            {
                _logger.Error($"DeleteCustomer - Error while Deleting customer of id - {id} with ex - {ex}");

                throw;
            }
        }

        public async Task<List<CustomerInfoDto>> GetAllCustomers()
        {
            _logger.Information($"Get All Customer executed");

            try
            {
                var customers = await _customerInfoRepos.GetAllCustomers();

                return customers.Select(n => _mapper.Map<CustomerInfoDto>(n)).ToList();
            }
            catch(Exception ex)
            {
                _logger.Error($"GetAllCustomers - Error while-{ex}");

                throw;

            }
        }

        public async Task<CustomerInfoDto> GetCustomerById(Guid id)
        {
            _logger.Information($"Get Customer by id - {id} executed");

            try
            {
                var customerById = await _customerInfoRepos.GetCustomerById(id);

                return _mapper.Map<CustomerInfoDto>(customerById);
            }
            catch(Exception ex)
            {
                _logger.Error($"GetCustomerById - Error while retriving customer of id - {id} with ex - {ex}");

                throw;
            }
        }

        public async Task<CustomerInfoDto> UpdateCustomer(CustomerInfoDto customerInfoDto)
        {
            _logger.Information($"UpdateCustomer executed for customer id - {customerInfoDto.Id}");

            try
            {

                var customerAlreadyExists =  await _customerInfoRepos.GetCustomerById(customerInfoDto.Id);

                if(customerAlreadyExists is null)
                {
                    _logger.Warning($"UpdateCustomer - Customer doesnot exists");

                    throw new Exception("Customer doesnot exists");
                }

                var customerModel = _mapper.Map<CustomerInfoModel>(customerInfoDto);

                await _customerInfoRepos.SaveCustomer(customerModel);

                return customerInfoDto;

            }
            catch(Exception ex)
            {
                _logger.Error($"UpdateCustomer - Error while updating the customer - {customerInfoDto} with ex - {ex}");

                throw;
            }
        }
    }
}

