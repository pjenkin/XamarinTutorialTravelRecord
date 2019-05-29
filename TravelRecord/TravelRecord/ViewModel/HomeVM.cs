using System;
using System.Collections.Generic;
using System.Text;
using TravelRecord.ViewModel.Commands;

namespace TravelRecord.ViewModel
{
    public class HomeVM
    {
        NavigationCommand NavCommand;           // our own ICommand implementation 12-101

        public HomeVM()
        {
            NavCommand = new NavigationCommand(this);
        }

        // hand-written bespoke method for navigating via ICommand implementation NavigationCommand
        public void Navigate()
        {

        }
    }
}
