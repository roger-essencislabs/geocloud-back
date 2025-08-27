using GeoCloudAI.API.Controllers;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectGeocloud
{
    public class InvoiceControllerTests
    {
        [Fact]
        public async Task GetInvoices_ReturnsOk_WhenInvoicesExist()
        {
            // Arrange
            var mockService = new Mock<IInvoiceService>();
            var invoices = new List<InvoiceDto>
            {
                new InvoiceDto { Id = 1, Invoice = "Basic", Amount = 100, Date = System.DateTime.Now, Status = "Subscribed" }
            };
            mockService.Setup(s => s.Get()).ReturnsAsync(invoices);

            var controller = new InvoicesController(mockService.Object);

            // Act
            var result = await controller.GetInvoices();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(invoices, okResult.Value);
        }

        [Fact]
        public async Task GetInvoices_ReturnsNotFound_WhenNoInvoicesExist()
        {
            // Arrange
            var mockService = new Mock<IInvoiceService>();
            mockService.Setup(s => s.Get()).ReturnsAsync((List<InvoiceDto>)null);

            var controller = new InvoicesController(mockService.Object);

            // Act
            var result = await controller.GetInvoices();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No invoices found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetInvoices_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var mockService = new Mock<IInvoiceService>();
            mockService.Setup(s => s.Get()).ThrowsAsync(new System.Exception("Erro de teste"));

            var controller = new InvoicesController(mockService.Object);

            // Act
            var result = await controller.GetInvoices();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Contains("Erro de teste", objectResult.Value.ToString());
        }
    }
}
