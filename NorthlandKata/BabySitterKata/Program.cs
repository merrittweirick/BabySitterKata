using System;

namespace BabySitterKata
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Parse("6pm");
            DateTime bed = DateTime.Parse("8pm");
            DateTime end = DateTime.Parse("3am");
            
            NightJob job = new NightJob(start, bed, end);
            Console.WriteLine("Hello World!");
        }
    }
}
