using DoctorPillMe.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace DoctorPillMe.ViewModels
{
    public class UsersListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<UserViewModel> Users { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand CreateUserCommand { get; protected set; }
        public ICommand DeleteUserCommand { get; protected set; }
        public ICommand SaveUserCommand { get; protected set; }
        public ICommand BackCommand { get; protected set; }
        UserViewModel? selectedUser;
        public INavigation? Navigation { get; set; }
        public UsersListViewModel()
        {
            Users = new ObservableCollection<UserViewModel>();
            CreateUserCommand = new Command(CreateUser);
            DeleteUserCommand = new Command(DeleteUser);
            SaveUserCommand = new Command(SaveUser);
            BackCommand = new Command(Back);
        }
        public UserViewModel? SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (selectedUser == value) return;
                UserViewModel temp = value;
                selectedUser = null;
                OnPropertyChanged("SelectedUser");
                Navigation?.PushAsync(new UserPage(temp));
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private void CreateUser() => Navigation?.PushAsync(new UserPage(new UserViewModel() { UsersListViewModel = this }));
        private void Back() => Navigation?.PopAsync();
        private void SaveUser(object UserObj)
        {
            if (UserObj is not UserViewModel User || User == null || !User.IsValid || Users.Contains(User)) return;
            Users.Add(User);
            Back();
        }
        private void DeleteUser(object UserObj)
        {
            if (UserObj is not UserViewModel User || User == null) return;
            Users.Remove(User);
            Back();
        }

    }
}
