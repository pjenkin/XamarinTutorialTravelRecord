using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    //public partial class HomePage : ContentPage           // 
    public partial class HomePage : TabbedPage
    {


        HomeVM viewModel;                   // ViewModel ready for use 12-102

		public HomePage ()
		{
			InitializeComponent ();

            viewModel = new HomeVM();       // instantiate ViewModel
            BindingContext = viewModel;     // make instantiated ViewModel available to XAML in page (via Command attribute)
		}
/*
        private void AddToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewTravelPage());          // segue over to new-travel page
        }
*/
    // click handler no longer needed after 'ing to ICommand/MVVM in 12-102
    }
}