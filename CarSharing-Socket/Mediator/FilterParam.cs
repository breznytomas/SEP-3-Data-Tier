using System;

namespace CarSharing_Database.Mediator
{
    public class FilterParam
    {
        public string Location { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}