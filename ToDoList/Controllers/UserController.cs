using ImageResizer.Plugins.Basic;
using Microsoft.AspNetCore.Mvc;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Service.Implementations;

namespace SocialProgrammer.Controllers;

public class UserController : Controller
{
    private readonly UserService booksService;

    public UserController(UserService booksService) =>
        this.booksService = booksService;

    public IActionResult Index() => View();


    [HttpGet]
    public async Task<List<UserEntity>> Get() =>
        await booksService.GetAsync();

    [HttpGet]
    public async Task<ActionResult<UserEntity>> Get(string id)
    {
        var book = await booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserEntity newBook)
    {
        await booksService.CreateAsync(newBook);

        return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
    }

    [HttpPost]
    public async Task<IActionResult> Update(string id, UserEntity updatedBook)
    {
        var book = await booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedBook.Id = book.Id;

        await booksService.UpdateAsync(id, updatedBook);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await booksService.RemoveAsync(id);

        return NoContent();
    }
}
