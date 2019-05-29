using System;
using System.Collections.Generic;
using System.Text;
using TravelRecord.ViewModel.Commands;

namespace TravelRecord.ViewModel
{
    public class HomeVM
    {
        public NavigationCommand NavCommand { get; set; }           // our own ICommand implementation 12-101, made a property also, thus bindable in XAML 12-102

        public HomeVM()
        {
            NavCommand = new NavigationCommand(this); 
        }

        // hand-written bespoke method for navigating via ICommand implementation NavigationCommand
        public async void Navigate()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NewTravelPage());           // go to a new Travel Page (back in navigation menu will also work)
        }
    }
}
