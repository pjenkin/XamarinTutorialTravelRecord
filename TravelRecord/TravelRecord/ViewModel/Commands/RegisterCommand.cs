using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravelRecord.Model;

namespace TravelRecord.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        private RegisterVM viewModel;                   // this ViewModel refernece member private here

        public event EventHandler CanExecuteChanged;

        public RegisterCommand(RegisterVM viewModel)
        {
            this.viewModel = viewModel;
        }


        public bool CanExecute(object parameter)
        {
            // throw new NotImplementedException();
            User user = (User)parameter;

            if (user == null)
            {
                return false;
            }

            if (user.Password == user.ConfirmPassword)
            {
                if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                {
                    return false;
                }
                // else
                return true;                // valid-looking credentials so go ahead and try

            }
            // else
            return false;
        }

        public void Execute(object parameter)
        {
            // throw new NotImplementedException();
            User user = (User)parameter;                // as ViewModel has Register method, will be receiving from parameter of command, hence cast of User here - cf RegisterVM::Register
            User.Register(user);                        // go ahead and register this user
        }
    }
}
