using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.App.SectionOne.ISP
{
    public interface IAnnualLeave
    {
        void ApplyAnnualLeave(int days);
    }

    public interface IHourlyLeave
    {
        void ApplyHourlyLeave(int hours);
    }

    public interface IFlexibleLeave
    {
        void ApplyFlexibleLeave(string reason);
    }

    public class FullTimeEmployeeX : IAnnualLeave, IHourlyLeave, IFlexibleLeave
    {
        public void ApplyAnnualLeave(int days)
        {
            Console.WriteLine($"Full-time employee applied for {days} days of annual leave.");
        }

        public void ApplyHourlyLeave(int hours)
        {
            Console.WriteLine($"Full-time employee applied for {hours} hours of hourly leave.");
        }

        public void ApplyFlexibleLeave(string reason)
        {
            Console.WriteLine($"Full-time employee applied for flexible leave due to: {reason}");
        }
    }

    public class PartTimeEmployeeX : IHourlyLeave
    {
        public void ApplyHourlyLeave(int hours)
        {
            Console.WriteLine($"Part-time employee applied for {hours} hours of hourly leave.");
        }
    }
}
