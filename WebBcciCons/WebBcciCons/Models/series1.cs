using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBcciCons.Models
{
    public class series1
    {
        public int MatchID { get; set; }
        public string MatchName { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public Nullable<System.DateTime> MatchDate { get; set; }
        public string Venue { get; set; }
        public Nullable<int> SeriesID { get; set; }
        public string SeriesName { get; set; }
        public Nullable<System.DateTime> SeriesStartDate { get; set; }
        public Nullable<System.DateTime> SeriesEndDate { get; set; }
    }
}