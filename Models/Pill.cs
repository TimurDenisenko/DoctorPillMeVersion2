using SQLite;

namespace DoctorPillMe.Models
{
    [Table("Pills")]
    public class Pill
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Amount {  get; set; } 
        public string? Unit { get; set; }
        public decimal Price { get; set; }
    }
}
