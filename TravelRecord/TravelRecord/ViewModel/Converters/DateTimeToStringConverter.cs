using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TravelRecord.ViewModel.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        // logic for conversion between Model (or ViewModel) and View in here
        // NB the 'object' types here
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // throw new NotImplementedException();
            string timeAgo = string.Empty;

            DateTimeOffset dateTime = (DateTimeOffset)value;
            DateTimeOffset now = DateTimeOffset.Now;
            var difference = now - dateTime;

            if (difference.TotalDays > 1)
            {
                timeAgo = $"{dateTime:d}";
            }
            else
            {
                if (difference.TotalSeconds < 60)
                    timeAgo = $"{Math.Round(difference.TotalSeconds)} seconds ago";
                else if (difference.TotalMinutes < 60)
                    timeAgo = $"{Math.Round(difference.TotalMinutes)} minutes ago";     // rounding with Math.Round
                else if (difference.TotalHours < 24)
                    timeAgo = $"{difference.TotalHours:0} hours ago";                   // rounding with format :0
                else
                // Otherwise, if neither more than a day nor less than a day ago...
                    timeAgo = "Yesterday";

                return timeAgo;
            }


            return timeAgo;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // throw new NotImplementedException();
            // Quite often ConvertBack is not used (from the View back to the Model)
            return DateTimeOffset.Now;          // No particular use for this as yet
        }
    }
}
