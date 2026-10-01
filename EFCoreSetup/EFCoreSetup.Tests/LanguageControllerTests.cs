using EFCoreSetupApp.Controllers;
using EFCoreSetupApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSetup.Tests;

public class LanguageControllerTests
{
    private static AppDBContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDBContext(options);
    }

    [Fact]
    public async Task GetById_ReturnsLanguage_WhenIdExists()
    {
        await using var context = CreateContext();
        context.Languages.Add(new Language { Id = 1, Title = "English" });
        await context.SaveChangesAsync();

        var controller = new LanguageController(context);

        var result = await controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var language = Assert.IsType<Language>(okResult.Value);
        Assert.Equal("English", language.Title);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenIdDoesNotExist()
    {
        await using var context = CreateContext();
        var controller = new LanguageController(context);

        var result = await controller.GetById(999);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetByName_ReturnsLanguage_WhenTitleMatches()
    {
        await using var context = CreateContext();
        context.Languages.Add(new Language { Id = 1, Title = "Urdu" });
        await context.SaveChangesAsync();

        var controller = new LanguageController(context);

        var result = await controller.GetByName("Urdu");

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var language = Assert.IsType<Language>(okResult.Value);
        Assert.Equal("Urdu", language.Title);
    }
}
