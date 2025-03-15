namespace NextHome.Domain.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsAvailable { get; set; }

        public User() { }

        public User(string id, string name, string email, bool isAvailable = true)
        {
            Id = id;
            Name = name;
            Email = email;
            CreatedAt = DateTime.UtcNow;
            IsAvailable = isAvailable;
        }
    }
}
