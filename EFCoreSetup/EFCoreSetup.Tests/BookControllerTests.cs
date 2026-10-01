using EFCoreSetupApp.Controllers;
using EFCoreSetupApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSetup.Tests;

public class BookControllerTests
{
    private static AppDBContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDBContext(options);
    }

    [Fact]
    public async Task GetById_ReturnsBook_WhenIdExists()
    {
        await using var context = CreateContext();
        context.Books.Add(new Book { Id = 1, Title = "Clean Code", LanguageId = 1, IsActive = true });
        await context.SaveChangesAsync();

        var controller = new BookController(context);

        var result = await controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var book = Assert.IsType<Book>(okResult.Value);
        Assert.Equal("Clean Code", book.Title);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenIdDoesNotExist()
    {
        await using var context = CreateContext();
        var controller = new BookController(context);

        var result = await controller.GetById(999);

        Assert.IsType<NotFoundResult>(result.Result);
    }
}
