using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Pinewood.Customer.Application.Core.DTOs;
using Pinewood.Customer.Application.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pinewood.Customer.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CustomerInfoController : Controller
    {
        private readonly ICustomerInfoService _customerInfoService;
        private readonly IValidator<CustomerInfoDto> _validator;

        public CustomerInfoController(IValidator<CustomerInfoDto> validator, ICustomerInfoService customerInfoService)
        {
            _validator = validator;
            _customerInfoService = customerInfoService;
        }

        [HttpPost]
        [Route("AddNewCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerInfoDto customerInfoDto)
        {
            ValidationResult result = await _validator.ValidateAsync(customerInfoDto);

            if (!result.IsValid)
            {
                return ValidationErrors(result);
            }
            var output = await _customerInfoService.CreateCustomer(customerInfoDto);

            return Ok(output);
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var output = await _customerInfoService.GetAllCustomers();

            return Ok(output);
        }


        [HttpPut]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody]CustomerInfoDto customerInfoDto)
        {
            ValidationResult result = await _validator.ValidateAsync(customerInfoDto);

            if (!result.IsValid)
            {
                return ValidationErrors(result);
            }

            var output = await _customerInfoService.UpdateCustomer(customerInfoDto);

            return Ok(output);
        }

        [HttpDelete]
        [Route("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomerById(Guid custId)
        {

           var output = await _customerInfoService.DeleteCustomer(custId);

            return Ok(output);

        }

        [HttpGet]
        [Route("GetCustomersById")]
        public async Task<IActionResult> GetCustomerById(Guid custId)
        {
            var output = await _customerInfoService.GetCustomerById(custId);

            return Ok(output);
        }

        private IActionResult ValidationErrors(ValidationResult result)
        {
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError(err.PropertyName, err.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }

    }
}