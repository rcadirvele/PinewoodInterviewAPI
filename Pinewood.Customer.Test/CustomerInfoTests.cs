using Pinewood.Customer.Application.Core.Interfaces;
using Pinewood.Customer.Application.Core.Models;
using Pinewood.Customer.Application.Core.Services;
using Pinewood.Customer.Application.Core.DTOs;

namespace Pinewood.Customer.Test;

public class Tests
{
    private ICustomerInfoService? _customerInfoService;
    private Mock<ICustomerInfoRepository> _customerReposMock;
    private Mock<ILogger> _loggerMock;
    private Mock<IMapper> _autoMapperMock;
    private static CustomerInfoDto _customerInfoDto;


    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _customerReposMock = new Mock<ICustomerInfoRepository>();
        _loggerMock = new Mock<ILogger>();
        _autoMapperMock = new Mock<IMapper>();
        _customerInfoService = new CustomerInfoService(_loggerMock.Object, _customerReposMock.Object, _autoMapperMock.Object);
    }

    [SetUp]
    public void Setup()
    {
        _customerInfoDto = new CustomerInfoDto
        {
            Id = Guid.NewGuid(),
            FirstName = "Ram",
            LastName = "Cadirvele",
            Phone = "7384740088",
            Email = "9009ram@gmail.com",
            Postcode = "CV3 1PG"
        };
    }

    [Test]
    public async Task CreateCustomer_Success_Test()
    {

        var customerInfoModel = new CustomerInfoModel();
        _autoMapperMock.Setup(m => m.Map<CustomerInfoModel>(_customerInfoDto)).Returns(customerInfoModel);

        var result = await _customerInfoService.CreateCustomer(_customerInfoDto);

        _customerReposMock.Verify(r => r.SaveCustomer(customerInfoModel), Times.Once);
        Assert.That(result, Is.EqualTo(_customerInfoDto));
    }

    [Test]
    public void CreateCustomer_Exception_Test()
    {
        string dbError = "exception from database";

        var customerInfoModel = new CustomerInfoModel();
        _autoMapperMock.Setup(m => m.Map<CustomerInfoModel>(_customerInfoDto)).Returns(customerInfoModel);
        _customerReposMock.Setup(r => r.SaveCustomer(customerInfoModel)).ThrowsAsync(new Exception(dbError));

        var ex = Assert.ThrowsAsync<Exception>(() => _customerInfoService.CreateCustomer(_customerInfoDto));
        Assert.That(ex.Message, Is.EqualTo(dbError));
        _loggerMock.Verify(l => l.Error(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task DeleteCustomer_customerExist_Success_Test()
    {
        var customerId = Guid.NewGuid();
        var customerInfoModel = new CustomerInfoModel { Id = customerId };
        _customerReposMock.Setup(r => r.GetCustomerById(customerId)).ReturnsAsync(customerInfoModel);

        var result = await _customerInfoService.DeleteCustomer(customerId);

        _customerReposMock.Verify(r => r.DeleteCustomer(customerId), Times.Once);
        Assert.That(result, Is.EqualTo(customerId));
    }

    [Test]
    public void DeleteCustomer_CustomerDoesNotExist_Success_Test()
    {
        var customerId = Guid.NewGuid();
        _customerReposMock.Setup(r => r.GetCustomerById(customerId)).ReturnsAsync((CustomerInfoModel)null);

        var ex = Assert.ThrowsAsync<Exception>(() => _customerInfoService.DeleteCustomer(customerId));

        Assert.That(ex.Message, Is.EqualTo("DeleteCustomer - Customer does not exists"));
        _loggerMock.Verify(l => l.Warning(It.IsAny<string>()), Times.Once);
        _customerReposMock.Verify(r => r.DeleteCustomer(It.IsAny<Guid>()), Times.Never);
    }

    [Test]
    public void DeleteCustomer_Exception_Test()
    {
        var dbError = "exception from database";
        var customerId = Guid.NewGuid();

        var customerInfoModel = new CustomerInfoModel { Id = customerId };
        _customerReposMock.Setup(r => r.GetCustomerById(customerId)).ReturnsAsync(customerInfoModel);
        _customerReposMock.Setup(r => r.DeleteCustomer(customerId)).ThrowsAsync(new Exception(dbError));

        var ex = Assert.ThrowsAsync<Exception>(() => _customerInfoService.DeleteCustomer(customerId));

        Assert.That(ex.Message, Is.EqualTo(dbError));
    }

    [Test]
    public async Task UpdateCustomer_CustomerExists_Test()
    {
        var existingCustomer = new CustomerInfoModel { Id = _customerInfoDto.Id };
        var updatedCustomer = new CustomerInfoModel { Id = _customerInfoDto.Id, FirstName = "Ram", LastName = "Cadirvele" };

        _customerReposMock.Setup(r => r.GetCustomerById(_customerInfoDto.Id)).ReturnsAsync(existingCustomer);
        _autoMapperMock.Setup(m => m.Map<CustomerInfoModel>(_customerInfoDto)).Returns(updatedCustomer);

        var result = await _customerInfoService.UpdateCustomer(_customerInfoDto);

        Assert.AreEqual(_customerInfoDto, result);
    }

    [Test]
    public void UpdateCustomer_Exception_Test()
    {
        var dbError = "exception from database";
        var existingCustomer = new CustomerInfoModel { Id = _customerInfoDto.Id };
        var updatedCustomer = new CustomerInfoModel { Id = _customerInfoDto.Id, FirstName = "Ram", LastName = "Cadirvele" };

        _customerReposMock.Setup(r => r.GetCustomerById(_customerInfoDto.Id)).ReturnsAsync(existingCustomer);
        _autoMapperMock.Setup(m => m.Map<CustomerInfoModel>(_customerInfoDto)).Returns(updatedCustomer);
        _customerReposMock.Setup(r => r.SaveCustomer(updatedCustomer)).ThrowsAsync(new Exception(dbError));

        var ex = Assert.ThrowsAsync<Exception>(() => _customerInfoService.UpdateCustomer(_customerInfoDto));

        Assert.AreEqual(dbError, ex.Message);
    }

    [Test]
    public void GetAllCustomers_Exception_Test()
    {
        var dbError = "exception from database";
        _customerReposMock.Setup(r => r.GetAllCustomers()).ThrowsAsync(new Exception(dbError));

        var ex = Assert.ThrowsAsync<Exception>(() => _customerInfoService.GetAllCustomers());

        Assert.AreEqual(dbError, ex.Message);
    }

}
