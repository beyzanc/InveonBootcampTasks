using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.App.SectionOne.ISP
{
    public interface ILeaveManager
    {
        void ApplyAnnualLeave(int days);
        void ApplyHourlyLeave(int hours);
        void ApplyFlexibleLeave(string reason);
    }

    public class FullTimeEmployee : ILeaveManager
    {
        public void ApplyAnnualLeave(int days)
        {
            Console.WriteLine($"Full-time employee applied for {days} days to use annual leave.");
        }

        public void ApplyHourlyLeave(int hours)
        {
            Console.WriteLine($"Full-time employee applied for {hours} hours to use hourly leave.");
        }

        public void ApplyFlexibleLeave(string reason)
        {
            Console.WriteLine($"Full-time employee applied for flexible leave because of: {reason}");
        }
    }

    public class PartTimeEmployee : ILeaveManager
    {
        public void ApplyAnnualLeave(int days)
        {
            throw new NotSupportedException("Part-time employees do not have annual leave.");
        }

        public void ApplyHourlyLeave(int hours)
        {
            Console.WriteLine($"Part-time employee applied for {hours} hours to use hourly leave.");
        }

        public void ApplyFlexibleLeave(string reason)
        {
            throw new NotSupportedException("Part-time employees do not have flexible leave.");
        }
    }
}
