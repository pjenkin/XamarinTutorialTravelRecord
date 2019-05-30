using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravelRecord.Model;
using TravelRecord.ViewModel.Commands;

namespace TravelRecord.ViewModel
{

    public class NewTravelVM : INotifyPropertyChanged
    {
        public PostCommand PostCommand { get; set; }        // public, xaml Binding-able property for the Command object to heave data around
        // public Post Post { get; set; }       // declare full instead so as to update via INotifyPropertyChanged

        private Post post;

        public Post Post
        {
            get { return post; }
            set {
                post = value;
                OnPropertyChanged("Post");      // fire away with event (last)
            }
        }

        // ditto for the other properties entered/changed by user

        private string experienceDescription;

        public string ExperienceDescription
        {
            get { return experienceDescription; }
            set {
                experienceDescription = value;
                Post = new Post
                {
                    ExperienceDescription = this.ExperienceDescription,
                    Venue = this.Venue
                };
                OnPropertyChanged("ExperienceDescription");      // fire away with event (last)
            }
        }

        private Venue venue;

        public Venue Venue
        {
            get { return venue; }
            set {
                venue = value;
                Post = new Post
                {
                    ExperienceDescription = this.ExperienceDescription,
                    Venue = this.Venue
                };

                OnPropertyChanged("Venue");      // fire away with event (last)
            }
        }
        // Needed to do the above to supply *updated* values via the ViewModel to the XAML


        public NewTravelVM()
        {
            PostCommand = new PostCommand(this);
            Venue = new Venue();
            Post = new Post();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void PublishPost(Post post)
        {
            try
            {
                Post.Insert(post);      // refactored to MVVM
                await App.Current.MainPage.DisplayAlert("Success", "Record successfully inserted", "OK");
            }


            catch (NullReferenceException nre)      // e.g. in case there's no category (null)
            {
                await App.Current.MainPage.DisplayAlert("Failure", "No record inserted", "OK");
            }
            catch (Exception ex)                // for more general exceptions that specifically null exceptions
            {
                await App.Current.MainPage.DisplayAlert("Failure", "No record inserted", "OK");
            }
        } 

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)     // important ! to avoid exceptions if no subscribers eg on startup
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));      // fire event if all's ready
            }
        }
    }  
}
