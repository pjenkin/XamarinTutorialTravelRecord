using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if (password.Text == confirmPasswordEntry.Text)
            {
                // register the user
                // register the user
                User user = new User()
                {
                    Email = email.Text,
                    Password = password.Text
                    // Id filled in automatically
                };

                await App.MobileService.GetTable<User>().InsertAsync(user);
                // User table name and type used to identify (easy)table within Azure db - class used to populate fields
            }
            else
            {
                await DisplayAlert("Error", "Passwords don't match", "OK");
            }
        }

        private void DummyButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}