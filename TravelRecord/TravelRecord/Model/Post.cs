﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecord.Model
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250)]
        public string ExperienceDescription { get; set; }        


        public string VenueName { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Distance { get; set; }


        // I'm not sure why we don't just have a Venue ID, CategoryID &c in here (TODO??)
    }
}
