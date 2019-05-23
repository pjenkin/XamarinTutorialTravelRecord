using SQLite;
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
    }
}
