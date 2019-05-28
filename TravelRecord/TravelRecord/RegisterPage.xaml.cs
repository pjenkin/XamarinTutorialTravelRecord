using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if (password.Text == confirmPasswordEntry.Text)
            {
                // register the user
            }
            else
            {
                DisplayAlert("Error", "Passwords don't match", "OK");
            }
        }

        private void DummyButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}