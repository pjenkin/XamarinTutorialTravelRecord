using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravelRecord.Model;

namespace TravelRecord.ViewModel.Commands
{
    public class PostCommand : ICommand
    {
        public NewTravelVM viewModel;                          // reference to ViewModel for NewTravel page stuff (public?)

        public event EventHandler CanExecuteChanged;

        public PostCommand(NewTravelVM viewModel)
        {
            this.viewModel = viewModel;
        }


        public bool CanExecute(object parameter)
        {
            // throw new NotImplementedException();
            var post = (Post)parameter;                 // Cast as Post - cf

            if (post != null)
            {
                if (string.IsNullOrEmpty(post.ExperienceDescription))
                {
                    return false;
                }

                if (post.Venue != null)
                {
                    return true;        // having established non-nullness of Venue value and ExperienceDescription and Post
                }

                return false;
            }
            return false;

        }

        public void Execute(object parameter)
        {
            // throw new NotImplementedException();
            var post = (Post)parameter;     // cf Command="{Binding PostCommand}"  CommandParameter="{Binding Post}" in NewTravelPage.xaml - sending thru this code
            viewModel.PublishPost(post);
        }
    }
}
