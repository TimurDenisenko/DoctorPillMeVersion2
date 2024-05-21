
using SQLite;

namespace DoctorPillMe.Models
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string? Name { get; set; }
        public string? Role { get; set; }
        public string? HashPassword { get; set; }
        public byte[]? Salt { get; set; }
    }
}
