using DoctorPillMe.Models;
using System.ComponentModel;

namespace DoctorPillMe.ViewModels
{
    public class UserViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        UsersListViewModel lvm;
        public User User { get; set; }
        public UserViewModel()
        {
            User = new User();
        }
        public UsersListViewModel UsersListViewModel
        {
            get { return lvm; }
            set
            {
                if (lvm == value) return;
                lvm = value;
                OnPropertyChanged("UsersListViewModel");
            }
        }
        public string Name
        {
            get { return User.Name; }
            set
            {
                if (User.Name == value) return;
                User.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public string HashPassword
        {
            get { return User.HashPassword; }
            set
            {
                if (User.HashPassword == value) return;
                User.HashPassword = value;
                OnPropertyChanged("HashPassword");
            }
        }
        public byte[] Salt
        {
            get { return User.Salt; }
            set
            {
                if (User.Salt == value) return;
                User.Salt = value;
                OnPropertyChanged("Salt");
            }
        }
        public bool IsValid
        {
            get
            {
                return new string[] { Name, HashPassword }.Any(x => !string.IsNullOrEmpty(x?.Trim()));
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
