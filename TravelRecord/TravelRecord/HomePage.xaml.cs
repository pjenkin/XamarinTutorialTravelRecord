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
    //public partial class HomePage : ContentPage           // 
    public partial class HomePage : TabbedPage
    {
		public HomePage ()
		{
			InitializeComponent ();
		}

        private void AddToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewTravelPage());          // segue over to new-travel page
        }
    }
}