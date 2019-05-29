using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TravelRecord.ViewModel.Commands
{
    public class RegisterNavigationCommand : ICommand
    {
        private MainVM viewModel;                           // this one private - use in constructor only
        public event EventHandler CanExecuteChanged;


        public RegisterNavigationCommand(MainVM viewModel)         // this command to use the Main view model - handy for Execute/Navigate
        {
            this.viewModel = viewModel;
        }


        public bool CanExecute(object parameter)
        {
            // throw new NotImplementedException();
            return true;
        }

        public void Execute(object parameter)
        {
            // throw new NotImplementedException();
            viewModel.Navigate();                       // use our ViewModel's Navigate method
        }
    }
}
