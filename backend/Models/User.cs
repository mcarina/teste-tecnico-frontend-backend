namespace User.Models
{
    public class UserModel
    {
        public UserModel(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; init; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public void Update(string name, string email)
        {
            Name = name;
            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }
    }

}
