using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.Model;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);    // prepare to set image source (9-73) (as soon as page formed)
            iconImage.Source = ImageSource.FromResource("TravelRecord.Assets.Images.pin_icon.png", assembly);

        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            /*
                        //string password = password.Text;
                        //string password = password.Text;
                        bool isEmailEmpty = string.IsNullOrEmpty(email.Text);
                        bool isPasswordEmpty = string.IsNullOrEmpty(password.Text);

                        if (isEmailEmpty || isPasswordEmpty)
                        {

                        }
                        else          // if password check worth doing
                        {
                            // Retrieve the User table from the Azure db, but only the specific user's record, if existent
                            var user = (await App.MobileService.GetTable<User>().Where(usr => usr.Email == email.Text).ToListAsync()).FirstOrDefault();

                            if (user != null)       // if user existent
                            {

                                App.user = user;                // set the app's current user to Azure-cloud-stored ID
                                if (user.Password == password.Text)
                                {
                                    await Navigation.PushAsync(new HomePage());       // cf segue & intent - will allow back navigation too via navigation bar on screen
                                }
                                else
                                {
                                    await DisplayAlert("Error", "Email or password are incorrect", "OK");
                                }
                            }
                            else
                            {
                                await DisplayAlert("Error", "There was an error while logging you in", "OK");

                            }
                        }
            */

            // Refactored for MVVM in 12-95
            bool canLogin = await User.Login(email.Text, password.Text);

            if (canLogin)
            {
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Error", "Try again", "Ok");
            }
        }

        private void RegisterUserButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());       // segue to register page
        }
    }
}
