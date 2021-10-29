using System;
namespace appt.Models.SeedWork.ValueObjects
{
    public class Hour
    {
        private int Minute { get; set; }
        private int Second { get; set; }

        public Hour(string hour)
        {
            var split = hour.Split(':', System.StringSplitOptions.TrimEntries);
            
            if (!int.TryParse(split[0], out int minute) || !int.TryParse(split[1], out int second))
            {
                throw new ArgumentException($"Argument could not be converted to type \"{typeof(int)}\"");
            }

            Minute = minute;
            Second = second;
        }

        public Hour(int minute, int second)
        {
            Minute = minute;
            Second = second;
        }

        public override string ToString()
        {
            return $"{Minute}:{Second}";
        }
    }
}