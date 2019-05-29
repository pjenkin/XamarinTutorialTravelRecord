using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TravelRecord.ViewModel.Commands
{
    public class NavigationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;        // boilerplate
        public HomeVM HomeViewModel { get; set; }           // use our hand-written ViewModel class

        public NavigationCommand(HomeVM homeVM)
        {
            HomeViewModel = homeVM;
        }


        public bool CanExecute(object parameter)
        {
            //throw new NotImplementedException();
            return true;                    // no checks needed in this case
        }

        public void Execute(object parameter)
        {
            // throw new NotImplementedException();
            // functionality to be defined in ViewModel (cf HomeVM in ViewModel folder)
            HomeViewModel.Navigate();
        }
    }
}
