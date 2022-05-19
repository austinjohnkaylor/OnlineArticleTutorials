using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UsersController(
        ILogger<UsersController> logger,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("All")]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Getting all users");
        IEnumerable<User> users = await _unitOfWork.Users.All();
        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetItem(Guid id)
    {
        _logger.LogInformation("Getting user by Id: {Guid}", id);
        User? item = await _unitOfWork.Users.GetById(id);

        if(item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        _logger.LogInformation("Creating a new user");
        if (!ModelState.IsValid) return new JsonResult("Something Went wrong") {StatusCode = 500};
        user.Id = Guid.NewGuid();

        await _unitOfWork.Users.Add(user);
        await _unitOfWork.Complete();

        return CreatedAtAction("GetItem", new {user.Id}, user);

    }

    [HttpPut]
    public async Task<IActionResult> UpdateItem(User user)
    {
        _logger.LogInformation("Updating User with Id {Guid} or inserting a new User", user.Id);

        await _unitOfWork.Users.Upsert(user);
        await _unitOfWork.Complete();

        // Following up the REST standard on update we need to return NoContent
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteItem(Guid id)
    {
        User? item = await _unitOfWork.Users.GetById(id);

        if(item == null)
            return BadRequest();
        
        _logger.LogInformation("Deleting User with Id {Guid}", id);

        await _unitOfWork.Users.Delete(id);
        await _unitOfWork.Complete();

        return Ok(item);
    }
}
