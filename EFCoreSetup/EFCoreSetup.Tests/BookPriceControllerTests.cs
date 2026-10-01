using EFCoreSetupApp.Controllers;
using EFCoreSetupApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSetup.Tests;

public class BookPriceControllerTests
{
    private static AppDBContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDBContext(options);
    }

    [Fact]
    public async Task GetById_ReturnsBookPrice_WhenIdExists()
    {
        await using var context = CreateContext();
        context.BookPrices.Add(new BookPrice { Id = 1, BookId = 1, CurrencyId = 1, Amount = 19.99m });
        await context.SaveChangesAsync();

        var controller = new BookPriceController(context);

        var result = await controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var bookPrice = Assert.IsType<BookPrice>(okResult.Value);
        Assert.Equal(19.99m, bookPrice.Amount);
    }
}
