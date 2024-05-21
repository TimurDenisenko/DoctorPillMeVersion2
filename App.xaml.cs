using DoctorPillMe.Models;
using DoctorPillMe.Views;

namespace DoctorPillMe
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "pills.db";
        public static Repository? database;
        public static Repository Database
        {
            get
            {
                if (database == null)
                {
                    database = new Repository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }
        [Obsolete]
        public App()
        {
            MainPage = new Shell
            {
                
                CurrentItem = new MainPage()
            };
        }
    }
}
