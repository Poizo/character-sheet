namespace CharacterApi.Controller;

using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{

    private readonly ILogger<UsersController> _logger;
    private CharacterDbContext _ctx;

    public UsersController(ILogger<UsersController> logger, CharacterDbContext ctx)
    {
        _logger = logger;
        _ctx = ctx;
    }

    [HttpGet(Name = "GetUsers")]
    public IEnumerable<UserDto> Get()
    {
        try
        {
            return _ctx.Users.Select(userDbe => new UserDto()
                {
                    Id = userDbe.Id,
                    Firstname = userDbe.Firstname,
                    Lastname = userDbe.Lastname,
                    Email = userDbe.Email,
                    CharacterIds = userDbe.Characters.Select(c => c.Id).ToList()
                }

            ).ToArray();
        }
        catch (System.Exception)
        {
            _logger.LogInformation("Catch ---- l'erreur ?");
            throw;
        }
    }


    [HttpPost("Login")]
    public ActionResult<UserDto> Login([FromBody] Credentials credentials)
    {
        try
        {
            var userToConnect = _ctx.Users.Where(u => u.Email == credentials.Email).Include(u => u.Characters).FirstOrDefault();
            if (userToConnect != null)
            {
                if (BCrypt.Verify(credentials.Password, userToConnect.Password))
                {
                    return Ok(new UserDto(){
                        Id = userToConnect.Id,
                        Firstname = userToConnect.Firstname,
                        Lastname = userToConnect.Lastname,
                        Email = userToConnect.Email,
                        CharacterIds = userToConnect.Characters.Select(c => c.Id).ToList()
                    });
                }
                return BadRequest("Wrong password");
            } else {
                return BadRequest("User does'nt exist");
            }
        }
        catch (System.Exception)
        {
            throw new Exception("something rotten in the state of denmark");
        }
    }


    [HttpPost("AddUser")]
    public UserDto AddUser([FromBody] AddUserDto addUser)
    {
        try
        {
            _ctx.Users.Add(new UserDbEntity(){
                Firstname = addUser.Firstname,
                Lastname = addUser.Lastname,
                Email = addUser.Email,
                Password = BCrypt.HashPassword(addUser.Password)
            });
            _ctx.SaveChanges();
            var newUser = _ctx.Users.Where(u => u.Email == addUser.Email).FirstOrDefault();

            return new() {
                Id = newUser.Id,
                Firstname = newUser.Firstname,
                Lastname = newUser.Lastname,
                Email = newUser.Email
            };
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}
