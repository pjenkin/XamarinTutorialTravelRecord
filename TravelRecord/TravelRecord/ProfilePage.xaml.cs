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
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))      // use database location static variable of App class
            {
                var postTable = conn.Table<Post>().ToList();                // count posts in db

                var categories = (from p in postTable
                                  orderby p?.CategoryId
                                  select p?.CategoryName ?? "No category given").Distinct().ToList();
                // don't show duplicated category name values (null conditional and coalescing added by PNJ)

                Dictionary<string, int> categoriesCount = new Dictionary<string, int>();        // make key/value dictionary of tally counts of categories

                foreach(var category in categories)
                {
                    var count = (from post in postTable
                                 where post.CategoryName == category
                                 select post).ToList().Count;               // LINQ used to count

                    categoriesCount.Add(category, count);                   // add dictionary entry
                }

                categoriesListView.ItemsSource = categoriesCount;           // link listview to query results (binding of data declared in xaml)

                postCountLabel.Text = postTable.Count.ToString();           // set text to number of posts
            }
        }
    }

    
}