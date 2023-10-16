public class UserDbEntity {
    public int Id { get; set; }
    public string Firstname {get; set;}
    public string Lastname {get; set;}
    public string Email {get; set;}
    public string Password {get; set;}
    public  ICollection<CharacterDbEntity> Characters {get; set;}
}
