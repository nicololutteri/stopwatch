using System;
using System.Linq;
using System.Text;

namespace Stopwatch
{
    public class Lap
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }

        public Lap()
        {

        }

        public Lap(DateTime startDate, DateTime endDate, string temp, string descrizione)
        {
            StartDate = startDate;
            EndDate = endDate;
            Time = temp;
            Description = descrizione;
        }
    }
}
