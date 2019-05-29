using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravelRecord.Model;
using TravelRecord.ViewModel.Commands;

namespace TravelRecord.ViewModel
{
    public class RegisterVM : INotifyPropertyChanged
    {
        private string email;

        public string Email
        {
            get { return email; }
            set
            {       // NB order of statements in set method in VM important to avoid loops - must be like so:
                email = value;
                User = new User()       // resetting User each time to ensure that input to the page is written into Model
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Email");		// OnPropertyChanged event fired *last*
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set {
                password = value;
                User = new User()
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Password");
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set {
                confirmPassword = value;
                User = new User()
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("ConfirmPassword");
            }
        }

        private User user;

        public User User
        {
            get { return user; }
            set {
                user = value;
/*
                User = new User()
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };
*/
// the above re re-setting User is not needed as this is the User member's own 'set' method !
                OnPropertyChanged("User");
            }
        }


        public RegisterCommand RegisterCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;       // implemented by CTRL.

        public RegisterVM()     // NB to set up Register Command for this ViewModel when this VM is set up firstly
        {
            RegisterCommand = new RegisterCommand(this);            // cf RegisterCommand::Execute
        }

        // hand-written
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        // hand-written
        public void Register(User user)
        {   // code copied in from Clicked event handler 12-104

            /* if (password.Text == confirmPasswordEntry.Text) // - must be same to have gotten here
            {
                User.Register(user);
            }
            else
            {
                await DisplayAlert("Error", "Passwords don't match", "OK");
            }
            */
            User.Register(user);
        }
    }
}
