using Microsoft.AspNetCore.Mvc;

namespace CharacterApi.Controller;

[ApiController]
[Route("[controller]")]
public class CharactersController : ControllerBase
{

    private readonly ILogger<CharactersController> _logger;
    private CharacterDbContext _ctx;

    public CharactersController(ILogger<CharactersController> logger, CharacterDbContext ctx)
    {
        _logger = logger;
        _ctx = ctx;
    }

    [HttpGet(Name = "GetCharacter")]
    public IEnumerable<CharacterDto> Get()
    {
        try
        {
            return _ctx.Characters.Select(characterDbe => new CharacterDto()
                {
                    Id = characterDbe.Id,
                    Name = characterDbe.Name,
                    Player = new UserDto
                    {
                        Id = characterDbe.Player.Id,
                        Firstname = characterDbe.Player.Firstname,
                        Lastname = characterDbe.Player.Lastname,
                        Email = characterDbe.Player.Email,
                        CharacterIds = characterDbe.Player.Characters.Select(c => c.Id).ToList()
                    },
                    Description = characterDbe.Description,
                    Strength = characterDbe.Strength,
                    Dexterity = characterDbe.Dexterity,
                    Constitution = characterDbe.Constitution,
                    Intelligence = characterDbe.Intelligence,
                    Wisdom = characterDbe.Wisdom,
                    Charisma = characterDbe.Charisma
                }

            ).ToArray();
        }
        catch (System.Exception)
        {
            _logger.LogInformation("Catch ---- l'erreur ?");
            throw;
        }
    }
}
