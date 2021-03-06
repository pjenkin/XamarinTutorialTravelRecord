﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
    // class for updating details of individual TravelRecord Post records
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PostDetail : ContentPage
	{
        Post selectedPost;                          // class member to track the record's data

        // public PostDetail ()
        public PostDetail (Post selectedPost)       // amend detail page constructor to take a record of the table in question (to populate class member)
		{
			InitializeComponent ();

            // initialise the text entry box wth existing data
            this.selectedPost = selectedPost;
            experienceDescriptionLabel.Text = selectedPost.ExperienceDescription;
		}

        // set data to whatever is in (ahem, not actually a label)
        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            selectedPost.ExperienceDescription = experienceDescriptionLabel.Text;
            /*
                        using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))     // use previously established location

            {
                int numRows = conn.Update(selectedPost);                                    // Update record - primary key ID used by system to identify record

                if (numRows > 0)
                {
                    // diagnostic alert
                    DisplayAlert("Success", "Record successfully updated", "OK");
                }
                else
                {
                    DisplayAlert("Failure", "No record updated", "OK");
                }                                                                                                       // Since SQLiteConnection (qv) is implementing IDisposable, we can, with a 'using' statement, 
             }                                                                                                      // safely leave out connection.Close call as Dispose will be automatically called
            */
            // From 11-92 no longer using SQLite - using Azure cloud db instead
            // update record in Azure db Posts table
            try
            {
                await App.MobileService.GetTable<Post>().UpdateAsync(selectedPost);
                await DisplayAlert("Success", "Record successfully updated", "OK");
            }
            catch (NullReferenceException nre)      // e.g. in case there's no category (null)
            {
                await DisplayAlert("Failure", "No record updated", "OK");
            }
            catch (Exception ex)                // for more general exceptions that specifically null exceptions
            {
                await DisplayAlert("Failure", "No record updated", "OK");
            }

        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            /*
                        using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))     // use previously established location
                                                                                                       // Since SQLiteConnection (qv) is implementing IDisposable, we can, with a 'using' statement, 
                                                                                                       // safely leave out connection.Close call as Dispose will be automatically called
                 {
                    int numRows = conn.Delete(selectedPost);                                    // Delete record - primary key ID used by system to identify record

                    if (numRows > 0)
                    {
                        // diagnostic alert
                        DisplayAlert("Success", "Record successfully deleted", "OK");
                    }
                    else
                    {
                        DisplayAlert("Failure", "No record deleted", "OK");
                    }
                }

           */

            // From 11-92 no longer using SQLite - using Azure cloud db instead

            try
            {
                // Delete record from Azure db Posts table

                await App.MobileService.GetTable<Post>().DeleteAsync(selectedPost);
                await DisplayAlert("Success", "Record successfully deleted", "OK");
            }
            catch (NullReferenceException nre)      // e.g. in case there's no category (null)
            {
                await DisplayAlert("Failure", "No record deleted", "OK");
            }
            catch (Exception ex)                // for more general exceptions that specifically null exceptions
            {
                await DisplayAlert("Failure", "No record deleted", "OK");
            }


        }

        /*
         * NB renaming a class (even with rt-click, rename) can mess-up the event-handler autocomplete 
         * (caused ‘Ensure Event failed’ alert in VS).
         */
    }
}
 