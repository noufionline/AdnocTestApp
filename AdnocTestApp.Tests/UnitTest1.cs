//// Using xUnit + Moq
//using Xunit;
//using Moq;
//using Microsoft.AspNetCore.Mvc;
//using AdnocTestApp1.Controllers;   // adjust namespace
//using AdnocTestApp1.Models;        // adjust
//using AdnocTestApp1.Data;          // adjust
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;

//using Microsoft.Extensions.Logging;
//using AdnocTestApp1.Repositories;

//public class HomeControllerTests
//{
//    private HomeController _controller;
//    private Mock<IDepartmentRepository> _mockRepository;
//    private Mock<ILogger<HomeController>> _mockLogger;

//    public HomeControllerTests()
//    {
       
//        _mockRepository = new Mock<IDepartmentRepository>();
//        _mockLogger = new Mock<ILogger<HomeController>>();

//        // If your controller requires context, maybe you pass it via constructor
//        _controller = new HomeController(_mockLogger.Object, _mockRepository.Object);
//    }


//    private AppDbContext GetInMemoryDbContext()
//    {
//        var options = new DbContextOptionsBuilder<AppDbContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // unique DB per test
//            .Options;

//        return new AppDbContext(options);
//    }

//    [Fact]
//    public async Task CreateDepartment_AddsDepartmentToDb()
//    {
//        // Arrange
//        var context = GetInMemoryDbContext();
//        var repo = new DepartmentRepository(context);
//        var department = new Department { Name = "HR", Location = "Abu Dhabi" };

//        // Act
//        await repo.CreateAsync(department);
//        var result = await repo.GetAllAsync();

//        // Assert
//        Assert.Single(result);
//        Assert.Equal("HR", result[0].Name);
//    }

//    [Fact]
//    public async Task UpdateAsync_UpdatesDepartmentInDb()
//    {
//        // Arrange
//        using var context = GetInMemoryDbContext();
//        var repo = new DepartmentRepository(context);

//        var department = new Department { Name = "HR", Location = "Abu Dhabi" };
//        await repo.CreateAsync(department);

//        department = await repo.FindAsync(department.Id);

//        // Act
//        department.Name = "Human Resources";
//        department.Location = "Dubai";
//        var updated = await repo.UpdateAsync(department);

//        // Assert
//        var result = await repo.FindAsync(department.Id);
//        Assert.NotNull(result);
//        Assert.Equal("Human Resources", result!.Name);
//        Assert.Equal("Dubai", result.Location);
//    }

//    [Fact]
//    public async Task DeleteAsync_RemovesDepartmentFromDb()
//    {
//        // Arrange
//        using var context = GetInMemoryDbContext();
//        var repo = new DepartmentRepository(context);

//        var department = new Department { Name = "Finance", Location = "Sharjah" };
//        await repo.CreateAsync(department);

//        // Act
//        await repo.DeleteAsync(department);

//        // Assert
//        var result = await repo.FindAsync(department.Id);
//        Assert.Null(result);

//        var all = await repo.GetAllAsync();
//        Assert.Empty(all);
//    }
//}

