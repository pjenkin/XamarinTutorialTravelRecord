using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.Model;
using TravelRecord.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        //User user;                                          // for use with both BindingContext and for login to App
        // supposedly User not needed as of 12-204  (short lived or what? this is a bit nuts) as User inside viewModel RegisterVM
        RegisterVM viewModel;

        public RegisterPage()
        {
            InitializeComponent();

            // user = new User();
            // containerStackLayout.BindingContext = user;     // set Binding context of User for XAML elements
            // supposedly User not needed as of 12-204  (short lived or what? this is a bit nuts) as User inside viewModel RegisterVM


            viewModel = new RegisterVM();
            BindingContext = viewModel;                         // BindingContext of XAML page is this ViewModel
        }
/*
        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {

            if (password.Text == confirmPasswordEntry.Text)
            {
                // register the user
                // register the user
                
                /*
                User user = new User()
                {
                    Email = email.Text,
                    Password = password.Text
                    // Id filled in automatically
                };
                // not needed after User declared for binding context in 12-99
                */

                // Refactored for MVVM in 12-95 - into User class
                //await App.MobileService.GetTable<User>().InsertAsync(user);
/*
                User.Register(user);
            }
            else
            {
                await DisplayAlert("Error", "Passwords don't match", "OK");
            }
        S}
        */
        private void DummyButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}