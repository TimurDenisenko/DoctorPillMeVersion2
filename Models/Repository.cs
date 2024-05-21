
using SQLite;

namespace DoctorPillMe.Models
{
    public class Repository
    {
        SQLiteConnection database;
        public Repository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Pill>();
            database.CreateTable<User>();
            database.CreateTable<Reminder>();
        }
        public IEnumerable<Pill> GetPills() =>
            database.Table<Pill>().ToList();
        public Pill GetPill(int id) =>
            database.Get<Pill>(id);
        public int DeletePill(int id) =>
            database.Delete<Pill>(id);
        public int SavePill(Pill item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            return database.Insert(item);
        }
        public IEnumerable<User> GetUsers() =>
            database.Table<User>().ToList();
        public User GetUser(int id) =>
            database.Get<User>(id);
        public int DeleteUser(int id) =>
            database.Delete<User>(id);
        public int SaveUser(User item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            return database.Insert(item);
        }
        public IEnumerable<Reminder> GetReminders() =>
            database.Table<Reminder>().ToList();
        public Reminder GetReminder(int id) =>
            database.Get<Reminder>(id);
        public int DeleteReminder(int id) =>
            database.Delete<Reminder>(id);
        public int SaveReminder(Reminder item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            return database.Insert(item);
        }
    }
}
