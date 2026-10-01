using EFCoreSetupApp.Controllers;
using EFCoreSetupApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSetup.Tests;

public class CurrencyTypeControllerTests
{
    private static AppDBContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDBContext(options);
    }

    [Fact]
    public async Task GetAll_ReturnsAllSeededCurrencyTypes()
    {
        await using var context = CreateContext();
        context.CurrencyTypes.AddRange(
            new CurrencyType { Id = 1, Currency = "USD" },
            new CurrencyType { Id = 2, Currency = "PKR" });
        await context.SaveChangesAsync();

        var controller = new CurrencyTypeController(context);

        var result = await controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var currencyTypes = Assert.IsAssignableFrom<IEnumerable<CurrencyType>>(okResult.Value);
        Assert.Equal(2, currencyTypes.Count());
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenIdDoesNotExist()
    {
        await using var context = CreateContext();
        var controller = new CurrencyTypeController(context);

        var result = await controller.GetById(999);

        Assert.IsType<NotFoundResult>(result.Result);
    }
}
