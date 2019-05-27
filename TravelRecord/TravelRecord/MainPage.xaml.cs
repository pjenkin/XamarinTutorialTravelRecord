using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            //string password = password.Text;
            //string password = password.Text;
            bool isEmailEmpty = string.IsNullOrEmpty(email.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(password.Text);

            if (isEmailEmpty || isPasswordEmpty)
            {

            }
            else
            {
                Navigation.PushAsync(new HomePage());       // cf segue & intent - will allow back navigation too via navigation bar on screen
            }
        }
    }
}
