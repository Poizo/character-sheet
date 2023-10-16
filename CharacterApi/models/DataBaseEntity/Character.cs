public class CharacterDbEntity {
    public int Id {get; set;}
    public string Name {get; set;}
    public UserDbEntity Player {get; set;}
    public int PlayerId {get; set;}
    public string? Description {get; set;}
    public int Strength {get; set;}
    public int Dexterity {get; set;}
    public int Constitution {get; set;}
    public int Intelligence {get; set;}
    public int Wisdom {get; set;}
    public int Charisma {get; set;}
}
