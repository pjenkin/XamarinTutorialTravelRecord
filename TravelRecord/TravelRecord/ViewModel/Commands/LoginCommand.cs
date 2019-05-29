using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravelRecord.Model;

namespace TravelRecord.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public MainVM ViewModel { get; set; }

        public LoginCommand(MainVM viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            // throw new NotImplementedException();
            // Need to evaluate on-the-fly (CanExecute) whether to execute (whether login worth attempting)
            // NB CommandParameter="{Binding User}" in MainPage.xaml.cs (User member/parameter in MainVM class)
            var user = (User)parameter;             // cast as user (coming from XAML)

            if (user == null)
            {
                return false;
            }

            // could have used ? null conditional for user in if statement below? 12-103

            if (String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.Password))
            {
                return false;           // return false if invalid login string(s) - not worth executing
            }
            // else / otherwise
            return true;
        }

        public void Execute(object parameter)
        {
            // throw new NotImplementedException();
            ViewModel.Login();              // execute the ViewModel's login method (thence the User's login method)
        }
    }
}
