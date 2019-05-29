using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravelRecord.Model;
using TravelRecord.ViewModel.Commands;

namespace TravelRecord.ViewModel
{
    public class MainVM : INotifyPropertyChanged            // for updating something or other, difficult to hear what/why he's saying of, in 12-103
    {
        // public User User { get; set; }                      // jumping around the code like a frog 
        private User user;                                      

        public User User
        {
            get { return user; }
            set {
                user = value;
                OnPropertyChanged("User");
            }
        }

        // get and set so that can be used with Binding in XAML
        public LoginCommand LoginCommand { get; set; }                              
        public RegisterNavigationCommand RegisterNavigationCommand { get; set; }

        // full properties, for use in Binding & MVVM to update properties

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value;
                User = new User                         // ensure that User object updated when login credentials changed
                {
                    Email = this.Email,
                    Password = this.Password
                };

                OnPropertyChanged("Email");
            }
        }

        private string password;

        public event PropertyChangedEventHandler PropertyChanged;       // CTRL+. from INotifyPropertyChanged

        public string Password
        {
            get { return password; }
            set { password = value;
                User = new User                         // ensure that User object updated when login credentials changed
                {
                    Email = this.Email,
                    Password = this.Password
                };

                OnPropertyChanged("Password");
            }
        }


        public MainVM()
        {
            User = new User();
            LoginCommand = new LoginCommand(this);
            RegisterNavigationCommand = new RegisterNavigationCommand(this);        // 12-104
            // MainVM (this) used to access Navigation command, inter alia
        }

        // bespoke command for login
        public async void Login()
        {
            bool canLogin = await User.Login(User.Email, User.Password);

            if (canLogin)
            {
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());    // use App.Current.whateverpage to access app navigation functionality
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Try again", "Ok");// use App.Current.whateverpage to access display alert functionality
            }
        }

        // hand-written
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)// important - to avoid error if no subscribers (eg on startup)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));      // use this with set methods
            }
        }

        // hand-written
        public async void Navigate()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

    }
}
