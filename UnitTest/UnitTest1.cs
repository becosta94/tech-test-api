

using FluentAssertions;
using Moq;
using tech_test_payment_api.Contexts;
using tech_test_payment_api.Interfaces;
using tech_test_payment_api.Models;
using tech_test_payment_api.Repository;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task FindSale()
        {
            var saleToTest = new Sales(DateTime.Now, 1, 1, "Tesoura, Cola", SaleStatus.WaitingPayment);
            var mockUserService0 = new Mock<ISaleUpdateStatus>();
            var mockUserService1 = new Mock<ISaleFinder>();
            var mockUserService2 = new Mock<ISaleFactory>();
            mockUserService1.Setup(services => services.Find(1))
                            .Returns(saleToTest);

            var salecontroller = new SalesController(mockUserService0.Object, mockUserService1.Object, mockUserService2.Object);


            var result = await salecontroller.ConsultSale(1);

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<Sales>();
            objectResult.Value.Should().Be(saleToTest);
        }

        [Fact]
        public async Task CreateNewSale()
        {
            var saleToTest = new Sales(DateTime.Now, 1, 1, "Tesoura, Cola", SaleStatus.WaitingPayment);

            var mockUserService0 = new Mock<ISaleUpdateStatus>();
            var mockUserService1 = new Mock<ISaleFinder>();
            var mockUserService2 = new Mock<ISaleFactory>();
            mockUserService2.Setup(services => services.Create(1, "Bernardo", 113, "be@gmail.com", "33210022", "Tesoura, Cola"))
                            .Returns(saleToTest);

            var salecontroller = new SalesController(mockUserService0.Object, mockUserService1.Object, mockUserService2.Object);

            var result = await salecontroller.NewSale(1, "Bernardo", 113, "be@gmail.com", "33210022", "Tesoura, Cola");


            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<Sales>();
            objectResult.Value.Should().Be(saleToTest);
        }

        [Fact]
        public async Task UpdateStatusSaleNotFound()
        {
            var saleToTest = new Sales(DateTime.Now, 1, 1, "Tesoura, Cola", SaleStatus.WaitingPayment);
            var mockUserService0 = new Mock<ISaleUpdateStatus>();
            var mockUserService1 = new Mock<ISaleFinder>();
            var mockUserService2 = new Mock<ISaleFactory>();
            mockUserService0.Setup(services => services.Update(500, SaleStatus.PaymentAccepted))
                            .Returns(1);

            var salecontroller = new SalesController(mockUserService0.Object, mockUserService1.Object, mockUserService2.Object);

            var resultUpdate = await salecontroller.UpdateStatus(500, SaleStatus.PaymentAccepted);

            resultUpdate.Should().BeOfType<BadRequestObjectResult>();
            var objectResult = (BadRequestObjectResult)resultUpdate;
            objectResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task UpdateStatusNotAllowed()
        {
            var saleToTest = new Sales(DateTime.Now, 1, 1, "Tesoura, Cola", SaleStatus.WaitingPayment);
            var mockUserService0 = new Mock<ISaleUpdateStatus>();
            var mockUserService1 = new Mock<ISaleFinder>();
            var mockUserService2 = new Mock<ISaleFactory>();
            mockUserService0.Setup(services => services.Update(1, SaleStatus.SentToCarrier))
                            .Returns(2);

            var salecontroller = new SalesController(mockUserService0.Object, mockUserService1.Object, mockUserService2.Object);

            var resultUpdate = await salecontroller.UpdateStatus(1, SaleStatus.SentToCarrier);

            resultUpdate.Should().BeOfType<BadRequestObjectResult>();
            var objectResult = (BadRequestObjectResult)resultUpdate;
            objectResult.StatusCode.Should().Be(400);
        }
    }
}