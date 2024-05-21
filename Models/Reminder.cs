
using SQLite;

namespace DoctorPillMe.Models
{
    [Table("Reminder")]
    public class Reminder
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? User { get; set; }
        public string? Pill { get; set; }
        public int Count { get; set; }
        public TimeSpan Time { get; set; }
        public string? Day { get; set; }
    }
}
