using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using AdnocTestApp1.Controllers;   // adjust namespace
using AdnocTestApp1.Models;
using AdnocTestApp1.Repositories;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

public class HomeControllerTests
{
    [Fact]
    public async Task CreateDepartment_ValidModel_ReturnsSuccessJson()
    {
        // Arrange
        var mockRepo = new Mock<IDepartmentRepository>();
        var mockLogger = new Mock<ILogger<HomeController>>();
        var controller = new HomeController(mockLogger.Object,mockRepo.Object);
        var dept = new Department {Name = "HR", Location = "Abu Dhabi" };


        mockRepo.Setup(r => r.CreateAsync(It.IsAny<Department>()))
        .ReturnsAsync(dept);

        // Act
        var result = await controller.CreateDepartment(dept);

        // Assert
        var jsonResult = Assert.IsType<JsonResult>(result);
        var dict = Assert.IsAssignableFrom<IDictionary<string, object>>(jsonResult.Value);

        Assert.True((bool)dict["success"]);
         dept = Assert.IsType<Department>(dict["data"]);
        Assert.Equal("HR", dept.Name);
       

        // Verify repository interaction
        mockRepo.Verify(r => r.CreateAsync(It.IsAny<Department>()), Times.Once);
    }

    [Fact]
    public async Task UpdateDepartment_Valid_ReturnsUpdatedDepartment()
    {
        // Arrange
        var dept = new Department { Id = 2, Name = "Finance", Location = "Dubai" };
        var mockRepo = new Mock<IDepartmentRepository>();
        var mockLogger = new Mock<ILogger<HomeController>>();
        mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Department>()))
                .ReturnsAsync(dept);

        var controller = new HomeController( mockLogger.Object, mockRepo.Object);

        // Act
        var result = await controller.UpdateDepartment(dept);

        // Assert
        var jsonResult = Assert.IsType<JsonResult>(result);
        dynamic data = jsonResult.Value!;
        Assert.True((bool)data.success);
        Assert.Equal("Finance", (string)data.data.Name);

        mockRepo.Verify(r => r.UpdateAsync(dept), Times.Once);
    }

    [Fact]
    public async Task DeleteDepartment_Found_RemovesAndReturnsSuccess()
    {
        // Arrange
        var dept = new Department { Id = 3, Name = "IT", Location = "Sharjah" };
        var mockRepo = new Mock<IDepartmentRepository>();
        var mockLogger = new Mock<ILogger<HomeController>>();
        mockRepo.Setup(r => r.FindAsync(3)).ReturnsAsync(dept);

        var controller = new HomeController(mockLogger.Object, mockRepo.Object);

        // Act
        var result = await controller.DeleteDepartment(3);

        // Assert
        var jsonResult = Assert.IsType<JsonResult>(result);
        dynamic data = jsonResult.Value!;
        Assert.True((bool)data.success);

        mockRepo.Verify(r => r.FindAsync(3), Times.Once);
        mockRepo.Verify(r => r.DeleteAsync(dept), Times.Once);
    }

    [Fact]
    public async Task DeleteDepartment_NotFound_ReturnsErrorJson()
    {
        // Arrange
        var mockRepo = new Mock<IDepartmentRepository>();
        var mockLogger = new Mock<ILogger<HomeController>>();
        mockRepo.Setup(r => r.FindAsync(99)).ReturnsAsync((Department?)null);

        var controller = new HomeController(mockLogger.Object, mockRepo.Object);

        // Act
        var result = await controller.DeleteDepartment(99);

        // Assert
        var jsonResult = Assert.IsType<JsonResult>(result);
        dynamic data = jsonResult.Value!;
        Assert.False((bool)data.success);
        Assert.Equal("Not found", (string)data.message);

        mockRepo.Verify(r => r.FindAsync(99), Times.Once);
        mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Department>()), Times.Never);
    }
}