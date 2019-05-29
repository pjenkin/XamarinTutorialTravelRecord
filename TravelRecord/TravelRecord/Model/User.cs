using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelRecord.Model
{
    // class to represent a user
    public class User : INotifyPropertyChanged          // NB INotifyPropertyChanged - CTRL+. to implement PropertyChanged
    {
        /*
                //public Guid Id { get; set; }        // Azure ID column holding alphanumeric values
                public string Id { get; set; }        // Azure ID column holding alphanumeric values

                public string Email { get; set; }

                public string Password { get; set; }
        */
        // Refatored in 12-98 in favour of full property declarations with setters for use with INotifyPropertyChanged

        private string id;

        public string Id
        {
            get { return id; }
            set {
                    id = value;
                    OnPropertyChanged("Id");    // fire event for this property
            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set {
                email = value;
                OnPropertyChanged("Email");
            }
        }


        private string password;

        public string Password
        {
            get { return password; }
            set {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        // NB Confirm Password added to User model so as to help with Binding and MVVM 12-104
        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }



        public User()
        {
            // blank constructor with no parameters
        }

        /// <summary>
        ///  Constructor for User object
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public event PropertyChangedEventHandler PropertyChanged;       // from CTRL+. on INotifyPropertyChanged

        public static async Task<bool> Login(string email, string password)
        {
            //string password = password.Text;
            //string password = password.Text;
            bool isEmailEmpty = string.IsNullOrEmpty(email);
            bool isPasswordEmpty = string.IsNullOrEmpty(password);

            if (isEmailEmpty || isPasswordEmpty)
            {
                return false;
            }
            else          // if password check worth doing
            {
                // Retrieve the User table from the Azure db, but only the specific user's record, if existent
                var user = (await App.MobileService.GetTable<User>().Where(usr => usr.Email == email).ToListAsync()).FirstOrDefault();

                if (user != null)       // if user existent
                {

                    App.user = user;                // set the app's current user to Azure-cloud-stored ID
                    if (user.Password == password)
                    {
                        //await Navigation.PushAsync(new HomePage());       // cf segue & intent - will allow back navigation too via navigation bar on screen
                        return true;
                    }
                    else
                    {
                        //await DisplayAlert("Error", "Email or password are incorrect", "OK");
                        // TODO alerts in  View later
                        return false;
                    }
                }
                else
                {
                    // await DisplayAlert("Error", "There was an error while logging you in", "OK");
                    // TODO alerts in  View later
                    return false;

                }
            }
        }

        /// <summary>
        /// Register user by inserting record into database
        /// 
        /// Refactored for MVVM
        /// </summary>
        /// <param name="user"></param>
        public static async void Register(User user)
        {
                await App.MobileService.GetTable<User>().InsertAsync(user);
                // User table name and type used to identify (easy)table within Azure db - class used to populate fields
        }

        /// <summary>
        /// for INotifyPropertyChanged
        /// hand-written
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                // 'this' is 'sender' - 'propertyName' from setter
            }
        }

    }
}
