using System;

namespace SpecialSymbol.Models
{
    public class ArchivedPostsSummary
    {
        public DateTime Date
        {
            get
            {
                return new DateTime(Year, Month, 1);
            }
        }
        public int Count { get; internal set; }
        public int Month { get; internal set; }
        public int Year { get; internal set; }
    }
}