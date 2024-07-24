using Pinewood.Customer.Application.Core.Interfaces;
using Pinewood.Customer.Application.Core.Models;
using Pinewood.Customer.Application.Core.Services;
using Pinewood.Customer.Application.Core.DTOs;
using AutoMapper;
using Serilog;

namespace Pinewood.Customer.Test;

public class Tests
{
    private ICustomerInfoService? _customerServices;
    private Mock<ICustomerInfoService> _customerServicesMock;
    private Mock<ICustomerInfoRepository> _customerRepoServicesMock;
    private Mock<ILogger> _loggerMock;
    private Mock<IMapper> _autoMapperMock;
    private static CustomerInfoDto _customerInfoDto;
    private static CustomerInfoModel _customerInfoModel;


    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _customerRepoServicesMock = new Mock<ICustomerInfoRepository>();
        _customerServicesMock = new Mock<ICustomerInfoService>();
        _loggerMock = new Mock<ILogger>();
        _autoMapperMock = new Mock<IMapper>();
        _customerServices = new CustomerInfoService(_loggerMock.Object, _customerRepoServicesMock.Object, _autoMapperMock.Object);
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
    public async Task Calc_Add_Test_Success()
    {
        _customerInfoModel = new CustomerInfoModel
        {
            Id = Guid.NewGuid(),
            FirstName = "Ram",
            LastName = "Cadirvele",
            Phone = "7384740088",
            Email = "9009ram@gmail.com",
            Postcode = "CV3 1PG"
        };

        //Arrange

        _customerRepoServicesMock.Setup(x => x.SaveCustomer(_customerInfoModel));

        _customerServicesMock.Setup(x => x.CreateCustomer(_customerInfoDto)).ReturnsAsync(It.IsAny<CustomerInfoDto>());

        //Act
        var actual = await _customerServices.CreateCustomer(_customerInfoDto);

        //Assert

        Assert.That(actual.FirstName, Is.EqualTo(_customerInfoDto.FirstName));

    }
}
