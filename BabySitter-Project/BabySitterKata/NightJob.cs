using System;
using System.Collections.Generic;
using System.Text;

namespace BabySitterKata
{
    public class NightJob
    {
        
        public NightJob()
        {

        }
        public NightJob(DateTime startTime, DateTime bedTime, DateTime endTime) // basing the user input to be converted to DateTime before constructing class
        {
            if(DateTime.Compare(DefaultStart, startTime) <= 0 && DateTime.Compare(Midnight, endTime) > 0)// if entered time is after cutoff & before midnight
            {
                this.StartTime = RoundToNearestHour(startTime);
            }
            else if(DateTime.Compare(DefaultStart, startTime) <= 0 && DateTime.Compare(Midnight, endTime) <= 0) // if entered time is after cutoff & after midnight
            {
                this.StartTime = RoundToNearestHour(startTime);
                this.StartTime.AddDays(1);
            }
            else
            {
                this.StartTime = DefaultStart;
            }
            if(DateTime.Compare(DefaultEnd, endTime)<=0 && DateTime.Compare(Midnight,endTime)<=0)// if entered time is before cuttoff & entered time is after midnight
            {
                this.EndTime = RoundToNearestHour(endTime);
                this.EndTime.AddDays(1);
            }
            else if((DateTime.Compare(DefaultEnd, endTime) <= 0 && DateTime.Compare(Midnight, endTime) > 0)) // if enteredtime is before cuttoff & is not after midnight
            {
                this.EndTime = RoundToNearestHour(endTime);
            }
            else
            {
                this.EndTime = DefaultEnd;
            }

            this.BedTime = RoundToNearestHour(bedTime);
            if(DateTime.Compare(this.BedTime, this.Midnight)<0 && this.BedTime.Hour == 00)
            {

            }
            
        }
        // if start time and end time are not provided - assigned default times
        public NightJob(DateTime bedTime)
        {
            this.BedTime = RoundToNearestHour(bedTime);
        }
        
        private DateTime DefaultStart { get; set; } = DateTime.Parse("5 PM");
        private  DateTime DefaultEnd { get; set; } = DateTime.Parse("4am").AddDays(1);
        public DateTime DefaultBed { get; set; } = DateTime.Parse("8 PM");
        public DateTime StartTime { get; set; }  
        public DateTime Midnight { get; private set; } = DateTime.Parse("12 AM").AddDays(1); // Midnight never changes want it to be immutable (private set)
        public DateTime EndTime { get; set; } 
        public DateTime BedTime { get; set; }

        

        //method for rounding time using DateTime.Minute comparison
        public DateTime RoundToNearestHour(DateTime timeToConvert)
        {
            int[] morningArray = new int[] { 23, 00, 01, 02, 04 };
            List<int> MorningHours = new List<int>(morningArray);
            DateTime roundedTime;
            TimeSpan clearMinutes = new TimeSpan((timeToConvert.Hour), 0, 0);
            TimeSpan increaseOneHour = new TimeSpan((timeToConvert.Hour + 1), 0, 0);
            
            if (timeToConvert.Minute >= 30 || MorningHours.Contains(timeToConvert.Hour)) // if converted time is in the AM or is over 30 minute mark, clear minutes and add a day to its date
            {
                DateTime ranToNextDay = timeToConvert.AddDays(1);
                roundedTime = timeToConvert.Date + increaseOneHour;
                return roundedTime;

            }
            else if(timeToConvert.Minute>=30)
            {
                roundedTime = timeToConvert.Date + increaseOneHour;
                return roundedTime;
            }
            else
            {

                roundedTime = timeToConvert.Date + clearMinutes;
            }
            return roundedTime;
        }

        // method for amount made before bedtime - $12
        public double ChargeBeforeBed()
        {
            double rateBeforeBed;
            double rateBeforeMidnight;

            if(StartTime >= BedTime)// in case job starts at bedtime or after kid is asleep
            {
                return 0.00;
            }
            else if (BedTime> Midnight)// if bedtime is after midnight
            {
                TimeSpan hoursTillMidnight = Midnight - StartTime;
                TimeSpan hoursUntilBed = BedTime - Midnight;
                rateBeforeMidnight = hoursTillMidnight.Hours * 12.00;
                rateBeforeBed = hoursUntilBed.Hours * 16.00; // decided to give them the $16 rate instead of $8 rate because babysitters don't get paid enough as it is
                return (rateBeforeBed + rateBeforeMidnight);
            }
            else
            {
                TimeSpan hoursUntilBed = BedTime - StartTime;
                rateBeforeBed = hoursUntilBed.Hours * 12;
                return rateBeforeBed;
            }
        }
        
        // method for bedtime to midnight- $8
        public double ChargeFromBedToMidnight()
        {
            double rateBeforeMidnight;
           
            if(BedTime>= Midnight) // if this edgecase happens do nothing (handled in previous method)
            {
                return 0.00;
            }
            else
            {
                TimeSpan hoursUntilMidnight = Midnight - BedTime;
                rateBeforeMidnight = hoursUntilMidnight.Hours * 8;
                return rateBeforeMidnight;
            }
        }
        //method made for amount made before 4AM -$16
        public double ChargeFromMidnightToEnd()
        {
            double rateAfterMidnight;
           
            if(Midnight >= EndTime) // handled in previous methods do nothing
            {
                return 0.00; 
            }
            else
            {
                TimeSpan hoursUntilEnd = EndTime - Midnight;
                rateAfterMidnight = hoursUntilEnd.Hours * 16;
                return rateAfterMidnight;
            }
            
        }

        //nightly charge method
        public double NightlyCharge()
        {
            double chargeBeforeBed = ChargeBeforeBed();
            double chargeFromBedToMidnight = ChargeFromBedToMidnight();
            double chargeFromMidnightToEnd = ChargeFromMidnightToEnd();
            double nightlyCharge = chargeBeforeBed + chargeFromBedToMidnight + chargeFromMidnightToEnd;
            return nightlyCharge;
        }
    }
}
